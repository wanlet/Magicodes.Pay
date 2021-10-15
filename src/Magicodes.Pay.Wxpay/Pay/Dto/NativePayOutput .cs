﻿// ======================================================================
//   
//           Copyright (C) 2018-2020 湖南心莱信息科技有限公司    
//           All rights reserved
//   
//           filename : AppPayOutput.cs
//           description :
//   
//           created by 雪雁 at  2018-07-30 17:45
//           Mail: wenqiang.li@xin-lai.com
//           QQ群：85318032（技术交流）
//           Blog：http://www.cnblogs.com/codelove/
//           GitHub：https://github.com/xin-lai
//           Home：http://xin-lai.com
//   
// ======================================================================

namespace Magicodes.Pay.Wxpay.Pay.Dto
{
    public class NativePayOutput
    {
        public string AppId { get; set; }

        public string MchId { get; set; }

        public string TimeStamp { get; set; }

        public string NonceStr { get; set; }

        public string PrepayId { get; set; }

        public string Package { get; set; }

        public string SignType { get; set; }
        public string PaySign { get; internal set; }

        public string CodeUrl { get; set; }
    }
}