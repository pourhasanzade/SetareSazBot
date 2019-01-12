using SetareSazBot.Domain.Enum;
using System.Configuration;

namespace SetareSazBot.Utility
{
    public static class EnumValue
    {
        public static string GetEnumValue(PositionTypeEnum type)
        {
            var result = "";
            if (type == PositionTypeEnum.Goalkeeper)
                result = "دروازه بان";
            else if (type == PositionTypeEnum.Forward)
                result = "مهاجم";
            else if (type == PositionTypeEnum.Defender)
                result = "مدافع";
            else if (type == PositionTypeEnum.Midfielder)
                result = "هافبک";

            return result;
        }

        public static string GetEnumValue(PopulationStatusEnum type)
        {
            var result = "";
            if (type == PopulationStatusEnum.City)
                result = "مرکز استان";
            else if (type == PopulationStatusEnum.Town)
                result = "شهرستان";
            else if (type == PopulationStatusEnum.Village)
                result = "روستا";            

            return result;
        }

        public static PositionTypeEnum? GetPositionEnumByValue(string input)
        {
            var result =  new PositionTypeEnum();
            if (input == "دروازه بان")
                result = PositionTypeEnum.Goalkeeper;
            else if (input == "مهاجم")
                result = PositionTypeEnum.Forward;
            else if (input == "مدافع")
                result = PositionTypeEnum.Defender;
            else if (input == "هافبک")
                result = PositionTypeEnum.Midfielder;

            return result;
        }

        public static PopulationStatusEnum? GetPopulationEnumByValue(string input)
        {
            var result = new PopulationStatusEnum();
            if (input == "مرکز استان")
                result = PopulationStatusEnum.City;
            else if (input == "شهرستان")
                result = PopulationStatusEnum.Town;
            else if (input == "روستا")
                result = PopulationStatusEnum.Village;

            return result;
        }

    }
}