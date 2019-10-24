using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Redmine.library.Core
{
    public interface  ISerializerHelper
    {
        JsonSerializerSettings SerializerSettings();
    }
}
