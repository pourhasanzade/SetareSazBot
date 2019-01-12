using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using SetareSazBot.API.Json.Input;
using SetareSazBot.Domain.Entity;
using SetareSazBot.Domain.Enum;
using SetareSazBot.Domain.Model;
using SetareSazBot.Service.Interface;
using SetareSazBot.Utility;

namespace SetareSazBot.API
{
    [RoutePrefix("api")]
    public class HomeController : BaseController
    {
        private readonly IMessengerService _messengerService;
        private readonly IKeypadService _keypadService;
        private readonly IConfigService _configService;
        private readonly IUserDataService _userDataService;
        private readonly IUserInfoService _userInfoService;
        private readonly IButtonService _buttonService;
        private readonly IUserInfoService _categoryService;
        private readonly IBankService _bankService;
        private readonly IProvinceService _provinceService;
        private static bool _isSendingMessage;


        public HomeController(IMessengerService messengerService,
            IKeypadService keypadService, IConfigService configService, IUserDataService userDataService,
            IUserInfoService userInfoService, IButtonService buttonService, IUserInfoService categoryService, 
            IBankService bankService, IProvinceService provinceService)
        {
            _messengerService = messengerService;
            _keypadService = keypadService;
            _configService = configService;
            _userDataService = userDataService;
            _userInfoService = userInfoService;
            _buttonService = buttonService;
            _categoryService = categoryService;
            _bankService = bankService;
            _provinceService = provinceService;
        }

        #region API

        [HttpGet, Route("getMessage")]
        public async Task<IHttpActionResult> GetMessages()
        {
            try
            {
                if (_isSendingMessage) return CustomResult();

                var config = await _configService.GetConfig();
                var result = await _messengerService.GetMessages(long.Parse(config.LastMessageId));

                if (result != null && result.Data.MessageList.Count > 0)
                {
                    _isSendingMessage = true;

                    var listOfMessageList = result.Data.MessageList.GroupBy(x => x.ChatId).Select(x => x.ToList()).ToList();

                    foreach (var messageList in listOfMessageList)
                    {

                        var myMessage = messageList.OrderByDescending(x => x.MessageId).FirstOrDefault();

                        if (myMessage?.ChatId == null /*|| myMessage.ChatId != "b_7149751_7"*/) continue;

                        await _configService.UpdateLastMessageId(myMessage.MessageId);
                        var tuple = await Command(myMessage);
                        await _messengerService.SendMessage(tuple.model);
                    }

                    _isSendingMessage = false;
                }
            }
            catch (Exception e)
            {
                _isSendingMessage = false;
                return CustomError(e);
            }
            return CustomResult();
        }

        [HttpPost, Route("receiveMessage")]
        public async Task<IHttpActionResult> ReceiveMessage([FromBody] GetMessagesInput json)
        {
            try
            {
                if (json.Type == MessageBehaviourTypeEnum.Message)
                    await _configService.UpdateLastMessageId(json.Mesage.MessageId);


                var tuple = await Command(json.Mesage);


                if (json.Type == MessageBehaviourTypeEnum.API)
                {

                    if (!tuple.toast) await _messengerService.SendMessage(tuple.model);
                    return Ok(new { bot_keypad = tuple.model.Keypad, text_message = tuple.toast ? tuple.model.Text : "" });
                }

                await _messengerService.SendMessage(tuple.model);
                return CustomResult();
            }
            catch (Exception exception)
            {
                return CustomError(exception);
            }
        }

        [HttpPost, Route("searchSelection")]
        public async Task<IHttpActionResult> SearchSelection([FromBody] SearchSelectionInput json)
        {
            try
            {
                if (json.SelectionId == "province") // استان
                {
                    var provinceList = await _provinceService.SearchProvinceAsync(json.SearchText, json.Limit);

                    var buttonSimpleList = new List<ButtonSimpleModel>();
                    foreach (var province in provinceList)
                    {
                        buttonSimpleList.Add(new ButtonSimpleModel
                        {
                            Type = ButtonSimpleTypeEnum.TextOnly,
                            Text = province.Name,
                            ImageUrl = null
                        });
                    }

                    return Ok(new { items = buttonSimpleList });
                }

                if (json.SelectionId == "city")  // شهر
                {
                    var account = await _userInfoService.GetUserInfo(json.ChatId);
                    //var userProvince = await _provinceService.GetProvinceAsync(account.ProvinceName);
                    var cityList = await _provinceService.SearchCityAsync(account.ProvinceId.Value, json.SearchText, json.Limit);

                    var buttonSimpleList = new List<ButtonSimpleModel>();
                    foreach (var city in cityList)
                    {
                        buttonSimpleList.Add(new ButtonSimpleModel
                        {
                            Type = ButtonSimpleTypeEnum.TextOnly,
                            Text = city.Name,
                            ImageUrl = null
                        });
                    }

                    return Ok(new { items = buttonSimpleList });
                }

 
                if (json.SelectionId == "bank")
                {
                    var bankList = await _bankService.SearchBank(json.SearchText, json.Limit);

                    var buttonSimpleList = new List<ButtonSimpleModel>();
                    foreach (var bank in bankList)
                    {
                        buttonSimpleList.Add(new ButtonSimpleModel
                        {
                            Type = ButtonSimpleTypeEnum.TextOnly,
                            Text = bank.Name,
                            ImageUrl = null
                        });
                    }

                    return Ok(new { items = buttonSimpleList });
                }


                return CustomResult();
            }
            catch (Exception exception)
            {
                return CustomError(exception);
            }
        }

        [HttpPost, Route("getSelection")]
        public async Task<IHttpActionResult> GetSelection([FromBody] GetSelectionInput json)
        {
            json.FirstIndex = json.FirstIndex - 1;

            try
            {
                if (json.SelectionId == "province") // استان
                {
                    var count = await _provinceService.GetProvinceCountAsync();

                    if (json.FirstIndex > count) return Ok(new { items = new List<ButtonSimpleModel>() });
                    if (json.LastIndex > count) json.LastIndex = count;

                    var provinceList = await _provinceService.GetProvinceListAsync(json.FirstIndex, json.LastIndex);

                    var buttonSimpleList = new List<ButtonSimpleModel>();
                    foreach (var province in provinceList)
                    {
                        buttonSimpleList.Add(new ButtonSimpleModel
                        {
                            Type = ButtonSimpleTypeEnum.TextOnly,
                            Text = province.Name,
                            ImageUrl = null
                        });
                    }

                    return Ok(new { items = buttonSimpleList });
                }

                if (json.SelectionId == "city")  // شهر
                {
                    var account = await _userInfoService.GetUserInfo(json.ChatId);
                    var count = await _provinceService.GetCityCountAsync(account.ProvinceId.Value);

                    if (json.FirstIndex > count) return Ok(new { items = new List<ButtonSimpleModel>() });
                    if (json.LastIndex > count) json.LastIndex = count;

                    var cityList = await _provinceService.GetCityListAsync(account.ProvinceId.Value, json.FirstIndex, json.LastIndex);

                    var buttonSimpleList = new List<ButtonSimpleModel>();
                    foreach (var city in cityList)
                    {
                        buttonSimpleList.Add(new ButtonSimpleModel
                        {
                            Type = ButtonSimpleTypeEnum.TextOnly,
                            Text = city.Name,
                            ImageUrl = null
                        });
                    }

                    return Ok(new { items = buttonSimpleList });
                }

                if (json.SelectionId == "bank")
                {
                    var count = await _bankService.GetBankCount();

                    if (json.FirstIndex > count) return Ok(new { items = new List<ButtonSimpleModel>() });
                    if (json.LastIndex > count) json.LastIndex = count;

                    var bankList = await _bankService.GetBankList(json.FirstIndex, json.LastIndex);

                    var buttonSimpleList = new List<ButtonSimpleModel>();
                    foreach (var bank in bankList)
                    {
                        buttonSimpleList.Add(new ButtonSimpleModel
                        {
                            Type = ButtonSimpleTypeEnum.TextOnly,
                            Text = bank.Name,
                            ImageUrl = null
                        });
                    }

                    return Ok(new { items = buttonSimpleList });
                }


                return CustomResult();
            }
            catch (Exception exception)
            {
                return CustomError(exception);
            }
        }


        #endregion

        #region Main Methods

        private async Task<(SendMessageInput model, bool toast)> Command(MessageModel myMessage)
        {
            var buttonId = myMessage.AuxData == null ? "" : myMessage.AuxData.ButtonId;
            var chatId = myMessage.ChatId;
            var messageText = myMessage.Text;
            var userDataEntity = await _userDataService.GetUserData(chatId);
            var model = new SendMessageInput
            {
                Keypad = new KeypadModel
                {
                    Rows = new List<KeypadRowModel>()
                },
                ReplyTimeout = Variables.ReplyTimeout,
                ChatId = chatId
            };

            if (string.IsNullOrEmpty(buttonId) && userDataEntity != null)
            {
                return await InvalidCommand(model, userDataEntity.StateId);
            }

            if (!string.IsNullOrEmpty(buttonId) && userDataEntity != null)
            {
                var button = await _buttonService.GetButton(buttonId);
                if (button == null || button.StateId != userDataEntity.StateId)
                    return await InvalidCommand(model, userDataEntity.StateId);
            }

            //start
            if (userDataEntity == null)
            {
                await _userDataService.Update(chatId, 1);

                model.Keypad = await _keypadService.GenerateButtonsAsync(1, new UserInfoEntity());
                model.Text = Messages.WelcomeMessage;
                return (model, false);
            }

            else if (buttonId == "1-1") // firsname
            {
                await _userDataService.Update(chatId, 1);

                var info = await _userInfoService.UpdateFirsName(chatId, messageText);
                model.Keypad = await _keypadService.GenerateButtonsAsync(1, info);
                model.Text = null;
                return (model, false);
            }

            else if (buttonId == "1-2") // lastname
            {
                await _userDataService.Update(chatId, 1);

                var info = await _userInfoService.UpdateLastName(chatId, messageText);
                model.Keypad = await _keypadService.GenerateButtonsAsync(1, info);
                model.Text = null;
                return (model, false);
            }

            else if (buttonId == "1-3") // povince
            {
                await _userDataService.Update(chatId, 1);
                var province = await _provinceService.GetProvinceAsync(messageText);

                var info = new UserInfoEntity();
                if (province != null)
                {
                    info = await _userInfoService.UpdateProvince(chatId, province.Id, province.Name);
                }
                else
                {
                    info = await _userInfoService.GetUserInfo(chatId);
                }
                
                model.Keypad = await _keypadService.GenerateButtonsAsync(1, info);
                model.Text = null;
                return (model, false);
            }

            else if (buttonId == "1-4") // city
            {
                await _userDataService.Update(chatId, 1);

                var info = await _userInfoService.GetUserInfo(chatId);

                var city = await _provinceService.GetCityAsync(info.ProvinceId.Value,messageText);
                if (city != null)
                {
                    info = await _userInfoService.UpdateCity(chatId, city.Id, city.Name);
                }

                model.Keypad = await _keypadService.GenerateButtonsAsync(1, info);
                model.Text = null;
                return (model, false);
            }

            else if (buttonId == "1-5") // population status
            {
                await _userDataService.Update(chatId, 1);
                var populationStatus = Utility.EnumValue.GetPopulationEnumByValue(messageText);

                var info = new UserInfoEntity();
                if(populationStatus.HasValue)
                {
                    info = await _userInfoService.UpdatePopulationStatus(chatId, populationStatus.Value, messageText);
                }
                else
                {
                    info = await _userInfoService.GetUserInfo(chatId);
                }
                
                model.Keypad = await _keypadService.GenerateButtonsAsync(1, info);
                model.Text = null;
                return (model, false);
            }

            else if (buttonId == "1-6") // mobile
            {
                await _userDataService.Update(chatId, 1);                

                var info = new UserInfoEntity();

                if (messageText.Length == 11 && messageText.StartsWith("09"))
                {
                    info = await _userInfoService.UpdateMobile(chatId, messageText);
                    model.Text = null;
                }
                else
                {
                    info = await _userInfoService.GetUserInfo(chatId);
                    model.Text = Messages.InvalidMobileNumber;
                }

                model.Keypad = await _keypadService.GenerateButtonsAsync(1, info);
                return (model, false);
            }

            else if (buttonId == "1-submit")
            {
                var info = await _userInfoService.GetUserInfo(chatId);
                model.Keypad = await _keypadService.GenerateButtonsAsync(2, info);

                await _userDataService.Update(chatId, 2);
                model.Text = null;
                return (model, false);
            }

            else if (buttonId == "2-1") // address
            {
                await _userDataService.Update(chatId, 2);

                var info = await _userInfoService.UpdateAddress(chatId, messageText);
                model.Keypad = await _keypadService.GenerateButtonsAsync(2, info);
                model.Text = null;
                return (model, false);
            }

            else if (buttonId == "2-2") // postalcode
            {
                await _userDataService.Update(chatId, 2);
                UserInfoEntity info;
                if (messageText.Length == 10)
                {
                    info = await _userInfoService.UpdatePostalCode(chatId, messageText);
                    model.Text = null;
                }
                else
                {
                    info = await _userInfoService.GetUserInfo(chatId);
                    model.Text = Messages.InvalidPostalCode;
                }
                    
                model.Keypad = await _keypadService.GenerateButtonsAsync(2, info);               
                return (model, false);
            }

            else if (buttonId == "2-3") // birthdate
            {
                await _userDataService.Update(chatId, 2);

                var info = await _userInfoService.UpdateBirthDate(chatId, messageText);
                model.Keypad = await _keypadService.GenerateButtonsAsync(2, info);
                model.Text = null;
                return (model, false);
            }

            else if (buttonId == "2-4") // national code
            {
                await _userDataService.Update(chatId, 2);

                var userInfo = new UserInfoEntity();
                if (messageText.Length != 10 || !messageText.IsValidNationalCode())
                {
                    userInfo = await _userInfoService.GetUserInfo(model.ChatId);
                    model.Text = Messages.InvalidNationalCode;
                }

                else
                {
                    userInfo = await _userInfoService.UpdateNationalCode(chatId, messageText);
                    model.Text = null;
                }

                model.Keypad = await _keypadService.GenerateButtonsAsync(2, userInfo);
                return (model, false);
            }

            else if (buttonId == "2-5") // position
            {
                await _userDataService.Update(chatId, 2);

                var position = EnumValue.GetPositionEnumByValue(messageText);
                UserInfoEntity info;
                if (position.HasValue)
                {
                    info = await _userInfoService.UpdatePosition(chatId, position.Value , messageText);                    
                }
                else
                {
                    info = await _userInfoService.GetUserInfo(chatId);
                }

                model.Keypad = await _keypadService.GenerateButtonsAsync(2, info);
                model.Text = null;
                return (model, false);

            }

            else if (buttonId == "2-submit")
            {
                await _userDataService.Update(chatId, 3);

                var info = await _userInfoService.GetUserInfo(chatId);
                model.Keypad = await _keypadService.GenerateButtonsAsync(3, info);
                if (string.IsNullOrEmpty(info.VideoSrc))//First time for upload
                {
                    model.Text = Messages.UploadGreeting;
                    await _messengerService.SendMessage(model);
                }

                model.Text = null;
                return (model, false);
            }

            else if (buttonId == "2-return")
            {
                await _userDataService.Update(chatId, 1);
                var info = await _userInfoService.GetUserInfo(chatId);
                model.Keypad = await _keypadService.GenerateButtonsAsync(1, info);
                model.Text = null;
                return (model, false);
            }

            else if (buttonId == "3-1") // video
            {
                await _userDataService.Update(chatId, 3);

                var upload = await UploadVideo(chatId, myMessage);

                if (!string.IsNullOrEmpty(upload.fileSrc))
                {
                    
                        var info = await _userInfoService.UpdateVideo(chatId, upload.fileSrc);
                        model.Keypad = await _keypadService.GenerateButtonsAsync(3, info);                        
                        return (model, false);                  
                }
                else
                {
                    var info = await _userInfoService.GetUserInfo(chatId);
                    model.Keypad = await _keypadService.GenerateButtonsAsync(3, info);
                    if(upload.error==UploadErrorEnum.OverSize) model.Text = Messages.OverSizeVideo;
                    else if (upload.error == UploadErrorEnum.InvalidExtension) model.Text = Messages.InvalidFileExtension;
                    else if (upload.error == UploadErrorEnum.UploadIssue) model.Text = Messages.FileNotUploaded;
                    return (model, true);
                }
            }

            //else if (buttonId == "3-submit")
            //{
            //    await _userDataService.Update(chatId, 3);

            //    var info = await _userInfoService.GetUserInfo(chatId);
            //    model.Keypad = await _keypadService.GenerateButtonsAsync(4, info);
            //    model.Text = null;
            //    return (model, false);
            //}

            else if (buttonId == "3-return")
            {
                await _userDataService.Update(chatId, 2);

                var info = await _userInfoService.GetUserInfo(chatId);
                model.Keypad = await _keypadService.GenerateButtonsAsync(2, info);
                model.Text = null;
                return (model, false);
            }

            return await InvalidCommand(model, userDataEntity.StateId);
        }

        private async Task<(SendMessageInput model, bool toast)> InvalidCommand(SendMessageInput model, long stateId, string text = null)
        {
            var info = await _userInfoService.GetUserInfo(model.ChatId);
            model.Keypad = await _keypadService.GenerateButtonsAsync(stateId, info);
            model.Text = text ?? Messages.InvalidCommand;
            return (model, true);
        }

        private async Task<(string fileSrc, UploadErrorEnum? error)> UploadVideo(string chatId, MessageModel message)
        {
            var minSize = 50* 1024*1024;
            var extension = Path.GetExtension(message.FileInline.FileName);
            if (!extension.IsValidVideoExtension())
                return (null, UploadErrorEnum.InvalidExtension);
            if(long.Parse(message.FileInline.Size)>minSize)
                return (null, UploadErrorEnum.OverSize);
            var downloadPath = System.Web.Hosting.HostingEnvironment.MapPath(Utility.Variables.DownloadAddress);
            var relativeAddress = chatId + "\\" + Guid.NewGuid() + extension;
            var filePath = downloadPath + relativeAddress;

            var download = await MyWebClient.Download(message.FileInline.FileUrl, filePath);

            if (!download)
                return (null,UploadErrorEnum.UploadIssue);
            return (relativeAddress, null);

        }

        
        #endregion

    }
}