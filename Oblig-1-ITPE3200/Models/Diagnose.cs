using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Oblig_1_ITPE3200.Models
{
    public class Diagnose
    {
        public int DiagnoseId { get; set; }
        public string DiagnoseNavn { get; set; }
        public string Description { get; set; }

        //[JsonIgnore] 特性修饰其中一个导航属性，该特性指示 Json.NET 在序列化时不遍历该导航属性。

        //[JsonIgnore]  
        public virtual List<SymptomDiagnose> SymptomDiagnoser { get; set; }
    }
}
