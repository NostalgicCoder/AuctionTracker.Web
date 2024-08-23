using AuctionTracker.Web.Interfaces;
using AuctionTracker.Web.Models;
using IgdbApi.Lib.Enum;
using System;
using System.Text.RegularExpressions;

namespace AuctionTracker.Web.Class
{
    public class GeneralHelper : IGeneralHelper
    {
        /// <summary>
        /// Convert the dateTime value into a more simplified version for display.
        /// - Full dateTime data retained in backend to allow for more powerful search features later
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public string ConvertDateTime(DateTime val)
        {
            switch (val.Month)
            {
                case 1:
                    return string.Format("Jan {0}", val.Year.ToString());
                    break;
                case 2:
                    return string.Format("Feb {0}", val.Year.ToString());
                    break;
                case 3:
                    return string.Format("Mar {0}", val.Year.ToString());
                    break;
                case 4:
                    return string.Format("Apr {0}", val.Year.ToString());
                    break;
                case 5:
                    return string.Format("May {0}", val.Year.ToString());
                    break;
                case 6:
                    return string.Format("Jun {0}", val.Year.ToString());
                    break;
                case 7:
                    return string.Format("Jul {0}", val.Year.ToString());
                    break;
                case 8:
                    return string.Format("Aug {0}", val.Year.ToString());
                    break;
                case 9:
                    return string.Format("Sep {0}", val.Year.ToString());
                    break;
                case 10:
                    return string.Format("Oct {0}", val.Year.ToString());
                    break;
                case 11:
                    return string.Format("Nov {0}", val.Year.ToString());
                    break;
                case 12:
                    return string.Format("Dec {0}", val.Year.ToString());
                    break;
                default:
                    return string.Empty;
                    break;
            }
        }

        /// <summary>
        /// Validate that a decimal value matches a pattern like '10.29' or '1000.45' and fails with for eg '100.456' or '.45'
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public bool ValidDecimalNumber(decimal val)
        {
            if(Regex.IsMatch(val.ToString(), @"^[0-9]*[.][0-9]{2}$"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Convert the database version of the platform to a integer platform Id compatible with IGDB Rest API
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public int ResolvePlatformId(Product product)
        {
            int platformId = 0;

            switch (product.SelectedGamePlatform)
            {
                case "Atari ST":
                    platformId = (int)PlatformEnum.AtariST;
                    break;
                case "PC":
                    platformId = (int)PlatformEnum.PC;
                    break;
                case "Amiga":
                    platformId = (int)PlatformEnum.Amiga;
                    break;
                case "Spectrum":
                    platformId = (int)PlatformEnum.Spectrum;
                    break;
                case "Amstrad":
                    platformId = (int)PlatformEnum.Amstrad;
                    break;
                case "Xbox 360":
                    platformId = (int)PlatformEnum.Xbox360;
                    break;
                case "C64":
                    platformId = (int)PlatformEnum.C64;
                    break;
            }

            return platformId;
        }
    }
}