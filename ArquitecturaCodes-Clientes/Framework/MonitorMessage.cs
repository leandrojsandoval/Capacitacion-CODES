using System.Collections.Generic;

namespace RHSP.Framework {

    public class MonitorMessage {

        public IList<MonitorData> MonitorData;
        public string TimeStamp;
        public string Method;

        public MonitorMessage (string timeStamp,
                              string method,
                              IList<MonitorData> monitorData) {
            this.MonitorData = monitorData;
            this.TimeStamp = timeStamp;
            this.Method = method;
        }
    }

    public class MonitorData {

        public string Name { get; set; }
        public string Value { get; set; }

        public MonitorData (string name, string value) {
            this.Name = name;
            this.Value = value;
        }

    }

}
