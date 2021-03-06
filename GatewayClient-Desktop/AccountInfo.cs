﻿namespace GatewayClient
{
    /// <summary>
    /// 账号信息
    /// </summary>
    public struct AccountInfo
    {
        /// <summary>
        /// 账号
        /// </summary>
        public string Uid;
        /// <summary>
        /// IP
        /// </summary>
        public string Ip;
        /// <summary>
        /// 流量 单位MB
        /// </summary>
        public double flow;
        /// <summary>
        /// 流量格式化字符串，带单位
        /// </summary>
        public string Flow
        {
            get
            {
                return FromatFlow(flow);
            }
        }

        /// <summary>
        /// 余额 单位￥ 
        /// </summary>
        public double fee;
        /// <summary>
        /// 格式化余额
        /// </summary>
        public string Fee
        {
            get
            {
                return fee <= 0 ? "0" : fee.ToString("G4") + "元";
            }
        }

        /// <summary>
        /// 剩余流量
        /// </summary>
        public string RFlow
        {
            get
            {
                double rflow = 500 * fee + 5 * 1000 - flow;
                return FromatFlow(rflow);
            }
        }

        public string FreeFlow
        {
            get
            {
                double free = 5000 - flow;
                return FromatFlow(free);
            }
        }
        /// <summary>
        /// 时间 单位 min
        /// </summary>
        public double time;
        /// <summary>
        /// 时间
        /// </summary>
        public string Time
        {
            get
            {
                if (time < 60)
                {
                    return time.ToString() + "分";
                }
                else if (time < 24 * 60)
                {
                    return ((int)time / 60).ToString() + "时" + (time % 60).ToString() + "分";
                }
                else
                {
                    return ((int)time / (1440)).ToString() + "天" + (time % 1440 / 60).ToString("F0") + "时" + (time % 1440 % 60).ToString() + "分";
                }
            }
        }

        /// <summary>
        /// 时间 单位MB/s
        /// </summary>
        public double speed;
        /// <summary>
        /// 格式化速度
        /// </summary>
        public string Speed
        {
            get
            {
                if (speed <= 0)
                {
                    return "0";
                }
                else if (speed * 1024 < 1)
                {
                    return (speed * 1024 * 1024).ToString("G4") + "B/s";
                }
                else if (speed < 1)
                {
                    return (speed * 1024).ToString("G4") + "KB/s";
                }
                else if (speed < 1000)
                {
                    return speed.ToString("G4") + "MB/s";
                }
                else
                {
                    return (speed / 1000).ToString("G4") + "GB/s";
                }


            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="flow"></param>
        /// <param name="fee"></param>
        /// <param name="time"></param>
        public AccountInfo(string uid = null, double flow = 0, double fee = 0, double time = 0)
        {
            Uid = uid;
            this.flow = flow;
            this.fee = fee;
            this.time = time;
            this.speed = 0;
            this.Ip = "";
        }

        public string FromatFlow(double Flow)
        {
            if (Flow <= 0)
            {
                return "0";
            }
            else if (Flow < 1)
            {
                //<1MB单位KB
                return (Flow * 1024).ToString("G4") + "K";
            }
            else if (Flow < 1000)
            {
                //<1GB 单位用MB
                return Flow.ToString("G4") + "M";
            }
            else
            {
                //网关计费单位1G=1000M
                return (Flow / 1000).ToString("G4") + "G";
            }
        }
    }
}
