//-----------------------------------------------------------------------
// <copyright file="<file>.cs" company="The Outercurve Foundation">
//    Copyright (c) 2011, The Outercurve Foundation.
//
//    Licensed under the MIT License (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//      http://www.opensource.org/licenses/mit-license.php
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
// </copyright>
// <author>Nathan Totten (ntotten.com), Jim Zimmerman (jimzimmerman.com) and Prabir ShresTha (prabir.me)</author>
// <website>https://github.com/facebook-csharp-sdk/simple-json</website>
//-----------------------------------------------------------------------

namespace SimpleJsonTests.DataContractTests
{
    using System.Runtime.Serialization;

    #region Fields

    [DataContract]
    public class DataContractPublicReadOnlyFields
    {
        [DataMember]
        public readonly string DataMemberWithoutName = "dmv";

        [DataMember(Name = "name")]
        public readonly string DatMemberWithName = "dmnv";

        [IgnoreDataMember]
        public readonly string IgnoreDataMember = "idm";

        public readonly string NoDataMember = "ndm";
    }

    [DataContract]
    public class DataContractPublicFields
    {
        [DataMember]
        public string DataMemberWithoutName = "dmv";

        [DataMember(Name = "name")]
        public string DatMemberWithName = "dmnv";

        [IgnoreDataMember]
        public string IgnoreDataMember = "idm";

        public string NoDataMember = "ndm";
    }

    // Supress is assigned by its value is never used
#pragma warning disable 0414
    [DataContract]
    public class DataContractPrivateReadOnlyFields
    {
        [DataMember]
        private readonly string DataMemberWithoutName = "dmv";

        [DataMember(Name = "name")]
        private readonly string DatMemberWithName = "dmnv";

        [IgnoreDataMember]
        private readonly string IgnoreDataMember = "idm";

        private readonly string NoDataMember = "ndm";
    }

    [DataContract]
    public class DataContractPrivateFields
    {
        [DataMember]
        private string DataMemberWithoutName = "dmv";

        [DataMember(Name = "name")]
        private string DatMemberWithName = "dmnv";

        [IgnoreDataMember]
        private string IgnoreDataMember = "idm";

        private string NoDataMember = "ndm";
    }

#pragma warning restore 0414
    #endregion

    #region Getter

    [DataContract]
    public class DataContractPublicGetters
    {
        [DataMember]
        public string DataMemberWithoutName { get { return "dmv"; } }

        [DataMember(Name = "name")]
        public string DatMemberWithName { get { return "dmnv"; } }

        [IgnoreDataMember]
        public string IgnoreDataMember { get { return "idm"; } }

        public string NoDataMember { get { return "ndm"; } }
    }

    [DataContract]
    public class DataContractPrivateGetters
    {
        [DataMember]
        private string DataMemberWithoutName { get { return "dmv"; } }

        [DataMember(Name = "name")]
        private string DatMemberWithName { get { return "dmnv"; } }

        [IgnoreDataMember]
        private string IgnoreDataMember { get { return "idm"; } }

        private string NoDataMember { get { return "ndm"; } }
    }

    #endregion

    #region Setter

    [DataContract]
    public class DataContractPublicSetters
    {
        [DataMember]
        public string DataMemberWithoutName { set { } }

        [DataMember(Name = "name")]
        public string DatMemberWithName { set { } }

        [IgnoreDataMember]
        public string IgnoreDataMember { set { } }

        public string NoDataMember { set { } }
    }

    [DataContract]
    public class DataContractPrivateSetters
    {
        [DataMember]
        private string DataMemberWithoutName { set { } }

        [DataMember(Name = "name")]
        private string DatMemberWithName { set { } }

        [IgnoreDataMember]
        private string IgnoreDataMember { set { } }

        private string NoDataMember { set { } }
    }

    #endregion

    #region Getter/Setters
    [DataContract]
    public class DataContractEmitDefaultValuePublicGetterSetters
    {
        public DataContractEmitDefaultValuePublicGetterSetters()
        {
            DataMemberWithoutName = "dmv";
            DatMemberWithName = "dmnv";
    }

        [DataMember]
        public string DataMemberWithoutName { get; set; }

        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string DatMemberWithName { get; set; }

        [IgnoreDataMember]
        public string IgnoreDataMember { get; set; }

        public string NoDataMember { get; set; }
    }

    [DataContract]
    public class DataContractPublicGetterSetters
    {
        public DataContractPublicGetterSetters()
        {
            DataMemberWithoutName = "dmv";
            DatMemberWithName = "dmnv";
            IgnoreDataMember = "idm";
            NoDataMember = "ndm";
        }

        [DataMember]
        public string DataMemberWithoutName { get; set; }

        [DataMember(Name = "name")]
        public string DatMemberWithName { get; set; }

        [IgnoreDataMember]
        public string IgnoreDataMember { get; set; }

        public string NoDataMember { get; set; }
    }

    [DataContract]
    public class DataContractPrivateGetterSetters
    {
        public DataContractPrivateGetterSetters()
        {
            DataMemberWithoutName = "dmv";
            DatMemberWithName = "dmnv";
            IgnoreDataMember = "idm";
            NoDataMember = "ndm";
        }

        [DataMember]
        private string DataMemberWithoutName { get; set; }

        [DataMember(Name = "name")]
        private string DatMemberWithName { get; set; }

        [IgnoreDataMember]
        private string IgnoreDataMember { get; set; }

        private string NoDataMember { get; set; }
    }

    #endregion
}