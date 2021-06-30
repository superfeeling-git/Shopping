using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Shopping.WCF_2
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IService1”。
    [ServiceContract]
    public interface IService1
    {
        /// <summary>
        /// WebInvoke特性用于POST/PUT/DELETE方式
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [
            WebInvoke
            (
            BodyStyle = WebMessageBodyStyle.Wrapped,  //枚举：封装请求或响应
            Method ="POST",                           //请求方法：GET或POST
            RequestFormat = WebMessageFormat.Json,    //枚举：请求的数据格式：XML或JSON
            ResponseFormat = WebMessageFormat.Json,   //枚举：响应的数据格式：XML或JSON
            UriTemplate = ""                          //URL模板，相当于咱们写的WEBAPI的路由
            )
        ]

        //WebGet特性用于GET方式，具体参数和WebInvoke类似
        [WebGet]
        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        // TODO: 在此添加您的服务操作
    }


    // 使用下面示例中说明的数据约定将复合类型添加到服务操作。
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
