using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SetareSazBot.Domain.Entity;
using SetareSazBot.Domain.Enum;
using SetareSazBot.Domain.Model;
using SetareSazBot.Service.Interface;
using SetareSazBot.Utility;

namespace SetareSazBot.Service
{
    public class KeypadService : IKeypadService
    {
        private readonly IButtonService _buttonService;
        private const MessageBehaviourTypeEnum BehaviourType = MessageBehaviourTypeEnum.Message;

        public KeypadService(IButtonService buttonService)
        {
            _buttonService = buttonService;
        }

        public async Task<KeypadModel> GenerateButtonsAsync(long stateId, object myObject = null)
        {
            return await FixButtonsAsync(stateId, myObject);
        }

        private async Task<KeypadModel> GetKeypadAsync(long stateId)
        {
            var buttonList = await _buttonService.GetButtonList(stateId);
            buttonList = buttonList.OrderBy(x => x.Row).ThenBy(x => x.Order).ToList();

            var keypad = new KeypadModel() { Rows = new List<KeypadRowModel>() };
            foreach (var button in buttonList)
            {
                if (keypad.Rows.Count < button.Row)
                {
                    keypad.Rows.Add(new KeypadRowModel());
                    keypad.Rows[button.Row - 1].Buttons = new List<ButtonModel>();
                }

                var newButton = new ButtonModel
                {
                    Id = button.Code,
                    Type = button.Type,
                    ButtonView = new ButtonSimpleModel
                    {
                        Type = button.ViewType,
                        Text = button.Text,
                        ImageUrl = button.ImageUrl
                    },
                    BehaviourType = button.BehaviourType
                };


                if (button.Type == ButtonTypeEnum.Selection)
                {
                    newButton.ButtonSelection = JsonConvert.DeserializeObject<ButtonSelectionModel>(button.Data);
                }
                else if (button.Type == ButtonTypeEnum.Calendar)
                {
                    newButton.ButtonCalendar = JsonConvert.DeserializeObject<ButtonCalendarModel>(button.Data);
                    var pc = new PersianCalendar();
                    var now = $"{pc.GetYear(DateTime.Now)}-{pc.GetMonth(DateTime.Now)}-{pc.GetDayOfMonth(DateTime.Now)}";
                    newButton.ButtonCalendar.DefaultValue = now;
                }
                else if (button.Type == ButtonTypeEnum.NumberPicker)
                {
                    newButton.ButtonNumberPicker = JsonConvert.DeserializeObject<ButtonNumberPickerModel>(button.Data);
                }
                else if (button.Type == ButtonTypeEnum.StringPicker)
                {
                    newButton.ButtonStringPicker = JsonConvert.DeserializeObject<ButtonStringPickerModel>(button.Data);
                }
                else if (button.Type == ButtonTypeEnum.Location)
                {
                    newButton.ButtonLocation = JsonConvert.DeserializeObject<ButtonLocationModel>(button.Data);
                }
                else if (button.Type == ButtonTypeEnum.Alert)
                {
                    newButton.ButtonAlert = JsonConvert.DeserializeObject<ButtonAlertModel>(button.Data);
                }
                else if (button.Type == ButtonTypeEnum.Textbox)
                {
                    newButton.ButtonTextBox = JsonConvert.DeserializeObject<ButtonTextBoxModel>(button.Data);
                }
                else if (button.Type == ButtonTypeEnum.Payment)
                {
                    // handled in another method 
                }
                else if (button.Type == ButtonTypeEnum.Call)
                {
                    newButton.ButtonCall = JsonConvert.DeserializeObject<ButtonCallModel>(button.Data);
                }

                keypad.Rows[button.Row - 1].Buttons.Add(newButton);
            }

            return keypad;
        }

        private async Task<KeypadModel> FixButtonsAsync(long stateId, object myObject)
        {
            var keypad = new KeypadModel();

            if (stateId == 1)
            {
                var isCompleted = true;

                keypad = await GetKeypadAsync(1);
                var info = (UserInfoEntity)myObject;

                if (!string.IsNullOrEmpty(info.FirstName)){ SetImage(keypad, "1-1"); } else isCompleted = false;

                if (!string.IsNullOrEmpty(info.LastName)){ SetImage(keypad, "1-2"); } else isCompleted = false;

                if (info.ProvinceId.HasValue){ SetImage(keypad, "1-3"); } else isCompleted = false;

                if (info.CityId.HasValue) { SetImage(keypad, "1-4"); } else isCompleted = false;

                if (info.PopulationStatus.HasValue)
                {
                    SetDefaultValue(keypad, "1-5", EnumValue.GetEnumValue(info.PopulationStatus.Value));
                    SetImage(keypad, "1-5");
                }
                else
                {
                    SetDefaultValue(keypad, "1-5", "شهرستان");
                    isCompleted = false;
                }

                if (!string.IsNullOrEmpty(info.Mobile)) { SetImage(keypad, "1-6"); } else isCompleted = false;

                if (!isCompleted) SetAlert(keypad, "1-submit");
            }


            if (stateId == 2)
            {
                var isCompleted = true;

                keypad = await GetKeypadAsync(2);
                var info = (UserInfoEntity)myObject;

                if (!string.IsNullOrEmpty(info.Address)) { SetImage(keypad, "2-1"); } else isCompleted = false;

                if (!string.IsNullOrEmpty(info.PostalCode)) { SetImage(keypad, "2-2"); } else isCompleted = false;

                if (!string.IsNullOrEmpty(info.BirthDate))
                {
                    SetDefaultValue(keypad, "2-3", info.BirthDate);
                    SetImage(keypad, "2-3");
                }
                else
                {
                    var time = DateTime.Now;
                    string now = "" + time.Year + time.Month + time.Day;
                    isCompleted = false;
                    SetDefaultValue(keypad, "2-3", now);
                    isCompleted = false;
                }

                if (!string.IsNullOrEmpty(info.NationalCode)) { SetImage(keypad, "2-4"); } else isCompleted = false;

                if (info.PositionType.HasValue)
                {
                    SetDefaultValue(keypad, "2-5", EnumValue.GetEnumValue(info.PositionType.Value));
                    SetImage(keypad, "2-5");
                }
                else isCompleted = false;

                if (!isCompleted) SetAlert(keypad, "2-submit");
            }


            if (stateId == 3)
            {
                var isCompleted = true;

                keypad = await GetKeypadAsync(3);
                var info = (UserInfoEntity)myObject;

                if (!string.IsNullOrEmpty(info.VideoSrc)) { SetImage(keypad, "3-1"); } else isCompleted = false;

                if (!isCompleted) SetAlert(keypad, "3-submit");
            }

            return keypad;
        }

        private async Task AddExtraKeypadAsync(KeypadModel keypad, long stateId)
        {
            var extraKeypad = await GetKeypadAsync(stateId);
            foreach (var row in extraKeypad.Rows)
            {
                keypad.Rows.Add(row);
            }
        }

        private void SetImage(KeypadModel keypad, string buttonCode)
        {
            var button = GetButton(keypad, buttonCode);
            if (button == null) return;

            button.ButtonView.ImageUrl = Variables.DoneImageUrl;
            button.ButtonView.Type = ButtonSimpleTypeEnum.TextImgThu;
        }

        private void SetAlert(KeypadModel keypad, string buttonCode, string text = null)
        {
            var button = GetButton(keypad, buttonCode);
            if (button == null) return;

            button.Type = ButtonTypeEnum.Alert;
            button.ButtonAlert = new ButtonAlertModel { Message = text ?? Messages.Required };
        }

        private void SetDefaultValue(KeypadModel keypad, string buttonCode, string input)
        {
            var button = GetButton(keypad, buttonCode);
            if (button == null) return;

            if (button.Type == ButtonTypeEnum.Calendar)
                button.ButtonCalendar.DefaultValue = input;

            else if (button.Type == ButtonTypeEnum.Calendar)
                button.ButtonCalendar.DefaultValue = input;
            else if (button.Type == ButtonTypeEnum.StringPicker)
                button.ButtonStringPicker.DefaultValue = input;
            else if (button.Type == ButtonTypeEnum.NumberPicker)
                button.ButtonNumberPicker.DefaultValue = input;
        }


        private ButtonModel GetButton(KeypadModel keypad, string buttonCode)
        {
            return keypad.Rows.SelectMany(x => x.Buttons).ToList().FirstOrDefault(x => x.Id == buttonCode);
        }

    }
}