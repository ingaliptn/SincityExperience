using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace WebUi.Lib
{
    public static class ProfileLib
    {
        public static string GetUrl(int number)
        {
            const string json = @"[
    {""EscortName"":""Samantha"",""EscortId"":""2""},
    {""EscortName"":""Jessica"",""EscortId"":""3""},
    {""EscortName"":""Naomi"",""EscortId"":""4""},
    {""EscortName"":""Alexa"",""EscortId"":""10""},
    {""EscortName"":""Cindy"",""EscortId"":""5""},
    {""EscortName"":""Alice"",""EscortId"":""6""},
    {""EscortName"":""Capri"",""EscortId"":""11""},
    {""EscortName"":""Britni"",""EscortId"":""12""},
    {""EscortName"":""Dawn"",""EscortId"":""13""},
    {""EscortName"":""Alexis"",""EscortId"":""14""},
    {""EscortName"":""Claudia"",""EscortId"":""15""},
    {""EscortName"":""Jenna"",""EscortId"":""16""},
    {""EscortName"":""April"",""EscortId"":""17""},
    {""EscortName"":""Marina "",""EscortId"":""18""},
    {""EscortName"":""Julia"",""EscortId"":""19""},
    {""EscortName"":""Melissa"",""EscortId"":""20""},
    {""EscortName"":""Anise"",""EscortId"":""21""},
    {""EscortName"":""Jodi"",""EscortId"":""22""},
    {""EscortName"":""Yoko"",""EscortId"":""26""},
    {""EscortName"":""Jojo"",""EscortId"":""27""},
    {""EscortName"":""Maria"",""EscortId"":""28""},
    {""EscortName"":""Glaucia"",""EscortId"":""29""},
    {""EscortName"":""Lana"",""EscortId"":""30""},
    {""EscortName"":""Bia"",""EscortId"":""31""},
    {""EscortName"":""Mellisa"",""EscortId"":""32""},
    {""EscortName"":""Amanda"",""EscortId"":""33""},
    {""EscortName"":""Summer"",""EscortId"":""34""},
    {""EscortName"":""Maya"",""EscortId"":""35""},
    {""EscortName"":""Arina"",""EscortId"":""36""},
    {""EscortName"":""Trisha"",""EscortId"":""37""},
    {""EscortName"":""Raquel"",""EscortId"":""38""},
    {""EscortName"":""Kim"",""EscortId"":""39""},
    {""EscortName"":""Felicia"",""EscortId"":""41""},
    {""EscortName"":""Sandra"",""EscortId"":""42""},
    {""EscortName"":""Amalee"",""EscortId"":""43""},
    {""EscortName"":""Britni"",""EscortId"":""44""},
    {""EscortName"":""Briahna"",""EscortId"":""45""},
    {""EscortName"":""Candice"",""EscortId"":""46""},
    {""EscortName"":""Celine"",""EscortId"":""47""},
    {""EscortName"":""Madison"",""EscortId"":""48""},
    {""EscortName"":""Cami"",""EscortId"":""51""},
    {""EscortName"":""Jade"",""EscortId"":""52""},
    {""EscortName"":""Cherie"",""EscortId"":""53""},
    {""EscortName"":""Aleysa"",""EscortId"":""54""},
    {""EscortName"":""Anna"",""EscortId"":""55""},
    {""EscortName"":""Karina"",""EscortId"":""56""},
    {""EscortName"":""Linda"",""EscortId"":""57""},
    {""EscortName"":""Nika"",""EscortId"":""58""},
    {""EscortName"":""Stefani"",""EscortId"":""59""},
    {""EscortName"":""Kara"",""EscortId"":""60""},
    {""EscortName"":""Helena"",""EscortId"":""62""},
    {""EscortName"":""Maria"",""EscortId"":""61""},
    {""EscortName"":""Edna"",""EscortId"":""201""},
    {""EscortName"":""Diana"",""EscortId"":""202""},
    {""EscortName"":""Louise"",""EscortId"":""203""},
    {""EscortName"":""Denise"",""EscortId"":""204""},
    {""EscortName"":""Julie"",""EscortId"":""205""},
    {""EscortName"":""Debra"",""EscortId"":""206""},
    {""EscortName"":""Amy"",""EscortId"":""207""},
    {""EscortName"":""Patricia"",""EscortId"":""208""},
    {""EscortName"":""Jazzy"",""EscortId"":""301""},
    {""EscortName"":""Layla"",""EscortId"":""302""},
    {""EscortName"":""London"",""EscortId"":""303""},
    {""EscortName"":""Hazel"",""EscortId"":""305""},
    {""EscortName"":""Rose"",""EscortId"":""306""},
    {""EscortName"":""Liza"",""EscortId"":""308""},
    {""EscortName"":""Cambria"",""EscortId"":""401""},
    {""EscortName"":""Alice"",""EscortId"":""400""},
    {""EscortName"":""Evelyn"",""EscortId"":""402""},
    {""EscortName"":""Giselle"",""EscortId"":""403""},
    {""EscortName"":""Heidi"",""EscortId"":""404""},
    {""EscortName"":""Livia"",""EscortId"":""405""},
    {""EscortName"":""Vivian"",""EscortId"":""406""},
    {""EscortName"":""Ashley"",""EscortId"":""3341""},
    {""EscortName"":""Carol"",""EscortId"":""3342""},
    {""EscortName"":""Cynthia"",""EscortId"":""3343""},
    {""EscortName"":""Ella"",""EscortId"":""3344""},
    {""EscortName"":""Kimberley"",""EscortId"":""3345""},
    {""EscortName"":""Lauren"",""EscortId"":""3346""},
    {""EscortName"":""Leah"",""EscortId"":""3347""},
    {""EscortName"":""Missy"",""EscortId"":""49""},
    {""EscortName"":""Linda"",""EscortId"":""3348""},
    {""EscortName"":""Alexa"",""EscortId"":""322""},
    {""EscortName"":""Riley"",""EscortId"":""50""}]";

            var model = JsonConvert.DeserializeObject<List<ProfileName>>(json);
            var name = model.Where(z => z.EscortId == number.ToString()).Select(z => z.EscortName).First().ToLower();
            return $"/profile/{name}.php";
        }
    }

    public class ProfileName
    {
        public string EscortName { get; set; }
        public string EscortId { get; set; }
    }
}
