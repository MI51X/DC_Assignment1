using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Registry.Models {
    public class PublishModel {
        public string Name { get; set; }
        public string Description { get; set; }
        public string APIendpoint { get; set; }
        public int NumberOfOperands { get; set; }
        public string OperandType { get; set; }
    }
}