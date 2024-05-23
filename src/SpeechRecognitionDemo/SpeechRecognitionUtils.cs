using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TencentCloud.Asr.V20190614;
using TencentCloud.Asr.V20190614.Models;
using TencentCloud.Common;
using TencentCloud.Common.Profile;

namespace SpeechRecognitionDemo
{
    /// <summary>
    /// 语音识别类库
    /// </summary>
    public class SpeechRecognitionUtils
    {
        /// <summary>
        /// 发起语音识别请求 获取taskId
        /// </summary>
        /// <param name="SecretId">SecretId 注意保密</param>
        /// <param name="SecretKey">SecretKey 注意保密</param>
        /// <returns></returns>
        public static string CreateTask(string SecretId,string SecretKey,string url)
        {
            try
            {
                // 实例化一个认证对象，入参需要传入腾讯云账户 SecretId 和 SecretKey，此处还需注意密钥对的保密
                // 代码泄露可能会导致 SecretId 和 SecretKey 泄露，并威胁账号下所有资源的安全性。以下代码示例仅供参考，建议采用更安全的方式来使用密钥，请参见：https://cloud.tencent.com/document/product/1278/85305
                // 密钥可前往官网控制台 https://console.cloud.tencent.com/cam/capi 进行获取
                Credential cred = new Credential
                {
                    SecretId = SecretId,
                    SecretKey = SecretKey
                };
                // 实例化一个client选项，可选的，没有特殊需求可以跳过
                ClientProfile clientProfile = new ClientProfile();
                // 实例化一个http选项，可选的，没有特殊需求可以跳过
                HttpProfile httpProfile = new HttpProfile();
                httpProfile.Endpoint = ("asr.tencentcloudapi.com");
                clientProfile.HttpProfile = httpProfile;

                // 实例化要请求产品的client对象,clientProfile是可选的
                AsrClient client = new AsrClient(cred, "", clientProfile);
                // 实例化一个请求对象,每个接口都会对应一个request对象
                CreateRecTaskRequest req = new CreateRecTaskRequest();
                req.EngineModelType = "16k_zh";
                req.ChannelNum = 1;
                req.SourceType = 0;
                req.Url = url;
                req.ResTextFormat = 1;
                // 返回的resp是一个CreateRecTaskResponse的实例，与请求对象对应
                CreateRecTaskResponse resp = client.CreateRecTaskSync(req);
                //string result = AbstractModel.ToJsonString(resp);
                string str = AbstractModel.ToJsonString(resp);
                return resp.Data.TaskId.ToString();
              
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return "";
            }
        }

        /// <summary>
        /// 根据任务id查询识别的结果 
        /// </summary>
        /// <param name="SecretId"></param>
        /// <param name="SecretKey"></param>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public static string SearchResultByTaskId(string SecretId, string SecretKey,string taskId)
        {
            try
            {
                // 实例化一个认证对象，入参需要传入腾讯云账户 SecretId 和 SecretKey，此处还需注意密钥对的保密
                // 代码泄露可能会导致 SecretId 和 SecretKey 泄露，并威胁账号下所有资源的安全性。以下代码示例仅供参考，建议采用更安全的方式来使用密钥，请参见：https://cloud.tencent.com/document/product/1278/85305
                // 密钥可前往官网控制台 https://console.cloud.tencent.com/cam/capi 进行获取
                Credential cred = new Credential
                {
                    SecretId = SecretId,
                    SecretKey = SecretKey
                };
                // 实例化一个client选项，可选的，没有特殊需求可以跳过
                ClientProfile clientProfile = new ClientProfile();
                // 实例化一个http选项，可选的，没有特殊需求可以跳过
                HttpProfile httpProfile = new HttpProfile();
                httpProfile.Endpoint = ("asr.tencentcloudapi.com");
                clientProfile.HttpProfile = httpProfile;

                // 实例化要请求产品的client对象,clientProfile是可选的
                AsrClient client = new AsrClient(cred, "", clientProfile);
                // 实例化一个请求对象,每个接口都会对应一个request对象
                DescribeTaskStatusRequest req = new DescribeTaskStatusRequest();
                req.TaskId = ulong.Parse(taskId);
                // 返回的resp是一个DescribeTaskStatusResponse的实例，与请求对象对应
                DescribeTaskStatusResponse resp = client.DescribeTaskStatusSync(req);
                string str = AbstractModel.ToJsonString(resp);
                return resp.Data.Result;
                // 输出json格式的字符串回包
                //Console.WriteLine(AbstractModel.ToJsonString(resp));
            }
            catch (Exception e)
            {
                //Console.WriteLine();
                return e.ToString();
            }
        }
    }
}
