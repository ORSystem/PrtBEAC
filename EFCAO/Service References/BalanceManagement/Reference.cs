﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EFCAO.BalanceManagement {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="AuthHeader", Namespace="http://schemas.datacontract.org/2004/07/WCFService")]
    [System.SerializableAttribute()]
    public partial class AuthHeader : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PasswordField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UserField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Password {
            get {
                return this.PasswordField;
            }
            set {
                if ((object.ReferenceEquals(this.PasswordField, value) != true)) {
                    this.PasswordField = value;
                    this.RaisePropertyChanged("Password");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string User {
            get {
                return this.UserField;
            }
            set {
                if ((object.ReferenceEquals(this.UserField, value) != true)) {
                    this.UserField = value;
                    this.RaisePropertyChanged("User");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Ades.BalanceSummary", Namespace="http://schemas.datacontract.org/2004/07/Anadefi")]
    [System.SerializableAttribute()]
    public partial class AdesBalanceSummary : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<System.DateTime> createdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string currencyField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<System.DateTime> dateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private ushort durationField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private ulong keyField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string labelField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private ushort lockTypeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string modelField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string statusField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private ushort typeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string unitsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<System.DateTime> updatedField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.DateTime> created {
            get {
                return this.createdField;
            }
            set {
                if ((this.createdField.Equals(value) != true)) {
                    this.createdField = value;
                    this.RaisePropertyChanged("created");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string currency {
            get {
                return this.currencyField;
            }
            set {
                if ((object.ReferenceEquals(this.currencyField, value) != true)) {
                    this.currencyField = value;
                    this.RaisePropertyChanged("currency");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.DateTime> date {
            get {
                return this.dateField;
            }
            set {
                if ((this.dateField.Equals(value) != true)) {
                    this.dateField = value;
                    this.RaisePropertyChanged("date");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public ushort duration {
            get {
                return this.durationField;
            }
            set {
                if ((this.durationField.Equals(value) != true)) {
                    this.durationField = value;
                    this.RaisePropertyChanged("duration");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public ulong key {
            get {
                return this.keyField;
            }
            set {
                if ((this.keyField.Equals(value) != true)) {
                    this.keyField = value;
                    this.RaisePropertyChanged("key");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string label {
            get {
                return this.labelField;
            }
            set {
                if ((object.ReferenceEquals(this.labelField, value) != true)) {
                    this.labelField = value;
                    this.RaisePropertyChanged("label");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public ushort lockType {
            get {
                return this.lockTypeField;
            }
            set {
                if ((this.lockTypeField.Equals(value) != true)) {
                    this.lockTypeField = value;
                    this.RaisePropertyChanged("lockType");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string model {
            get {
                return this.modelField;
            }
            set {
                if ((object.ReferenceEquals(this.modelField, value) != true)) {
                    this.modelField = value;
                    this.RaisePropertyChanged("model");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string status {
            get {
                return this.statusField;
            }
            set {
                if ((object.ReferenceEquals(this.statusField, value) != true)) {
                    this.statusField = value;
                    this.RaisePropertyChanged("status");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public ushort type {
            get {
                return this.typeField;
            }
            set {
                if ((this.typeField.Equals(value) != true)) {
                    this.typeField = value;
                    this.RaisePropertyChanged("type");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string units {
            get {
                return this.unitsField;
            }
            set {
                if ((object.ReferenceEquals(this.unitsField, value) != true)) {
                    this.unitsField = value;
                    this.RaisePropertyChanged("units");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.DateTime> updated {
            get {
                return this.updatedField;
            }
            set {
                if ((this.updatedField.Equals(value) != true)) {
                    this.updatedField = value;
                    this.RaisePropertyChanged("updated");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Ades.Property", Namespace="http://schemas.datacontract.org/2004/07/Anadefi")]
    [System.SerializableAttribute()]
    public partial class AdesProperty : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string codeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string valueField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string code {
            get {
                return this.codeField;
            }
            set {
                if ((object.ReferenceEquals(this.codeField, value) != true)) {
                    this.codeField = value;
                    this.RaisePropertyChanged("code");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string value {
            get {
                return this.valueField;
            }
            set {
                if ((object.ReferenceEquals(this.valueField, value) != true)) {
                    this.valueField = value;
                    this.RaisePropertyChanged("value");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="BalanceManagement.IBalanceManagement")]
    public interface IBalanceManagement {
        
        // CODEGEN : La génération du contrat de message depuis le message BalanceListRequest contient des en-têtes
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBalanceManagement/BalanceList", ReplyAction="http://tempuri.org/IBalanceManagement/BalanceListResponse")]
        EFCAO.BalanceManagement.BalanceListResponse BalanceList(EFCAO.BalanceManagement.BalanceListRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBalanceManagement/BalanceList", ReplyAction="http://tempuri.org/IBalanceManagement/BalanceListResponse")]
        System.Threading.Tasks.Task<EFCAO.BalanceManagement.BalanceListResponse> BalanceListAsync(EFCAO.BalanceManagement.BalanceListRequest request);
        
        // CODEGEN : La génération du contrat de message depuis le message BalanceCreateRequest contient des en-têtes
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBalanceManagement/BalanceCreate", ReplyAction="http://tempuri.org/IBalanceManagement/BalanceCreateResponse")]
        EFCAO.BalanceManagement.BalanceCreateResponse BalanceCreate(EFCAO.BalanceManagement.BalanceCreateRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBalanceManagement/BalanceCreate", ReplyAction="http://tempuri.org/IBalanceManagement/BalanceCreateResponse")]
        System.Threading.Tasks.Task<EFCAO.BalanceManagement.BalanceCreateResponse> BalanceCreateAsync(EFCAO.BalanceManagement.BalanceCreateRequest request);
        
        // CODEGEN : La génération du contrat de message depuis le message BalanceGetDataRequest contient des en-têtes
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBalanceManagement/BalanceGetData", ReplyAction="http://tempuri.org/IBalanceManagement/BalanceGetDataResponse")]
        EFCAO.BalanceManagement.BalanceGetDataResponse BalanceGetData(EFCAO.BalanceManagement.BalanceGetDataRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBalanceManagement/BalanceGetData", ReplyAction="http://tempuri.org/IBalanceManagement/BalanceGetDataResponse")]
        System.Threading.Tasks.Task<EFCAO.BalanceManagement.BalanceGetDataResponse> BalanceGetDataAsync(EFCAO.BalanceManagement.BalanceGetDataRequest request);
        
        // CODEGEN : La génération du contrat de message depuis le message BalanceSetDataRequest contient des en-têtes
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBalanceManagement/BalanceSetData", ReplyAction="http://tempuri.org/IBalanceManagement/BalanceSetDataResponse")]
        EFCAO.BalanceManagement.BalanceSetDataResponse BalanceSetData(EFCAO.BalanceManagement.BalanceSetDataRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBalanceManagement/BalanceSetData", ReplyAction="http://tempuri.org/IBalanceManagement/BalanceSetDataResponse")]
        System.Threading.Tasks.Task<EFCAO.BalanceManagement.BalanceSetDataResponse> BalanceSetDataAsync(EFCAO.BalanceManagement.BalanceSetDataRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="BalanceList", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class BalanceListRequest {
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="")]
        public EFCAO.BalanceManagement.AuthHeader AuthHeader;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public ulong companyKey;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=1)]
        public string modelName;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=2)]
        public string fromDate;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=3)]
        public string toDate;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=4)]
        public ushort type;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=5)]
        public ushort max;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=6)]
        public bool reverse;
        
        public BalanceListRequest() {
        }
        
        public BalanceListRequest(EFCAO.BalanceManagement.AuthHeader AuthHeader, ulong companyKey, string modelName, string fromDate, string toDate, ushort type, ushort max, bool reverse) {
            this.AuthHeader = AuthHeader;
            this.companyKey = companyKey;
            this.modelName = modelName;
            this.fromDate = fromDate;
            this.toDate = toDate;
            this.type = type;
            this.max = max;
            this.reverse = reverse;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="BalanceListResponse", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class BalanceListResponse {
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="")]
        public EFCAO.BalanceManagement.AuthHeader AuthHeader;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public EFCAO.BalanceManagement.AdesBalanceSummary[] BalanceListResult;
        
        public BalanceListResponse() {
        }
        
        public BalanceListResponse(EFCAO.BalanceManagement.AuthHeader AuthHeader, EFCAO.BalanceManagement.AdesBalanceSummary[] BalanceListResult) {
            this.AuthHeader = AuthHeader;
            this.BalanceListResult = BalanceListResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="BalanceCreate", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class BalanceCreateRequest {
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="")]
        public EFCAO.BalanceManagement.AuthHeader AuthHeader;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public ulong companyKey;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=1)]
        public string modelName;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=2)]
        public System.DateTime date;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=3)]
        public ushort type;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=4)]
        public string unit;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=5)]
        public string currency;
        
        public BalanceCreateRequest() {
        }
        
        public BalanceCreateRequest(EFCAO.BalanceManagement.AuthHeader AuthHeader, ulong companyKey, string modelName, System.DateTime date, ushort type, string unit, string currency) {
            this.AuthHeader = AuthHeader;
            this.companyKey = companyKey;
            this.modelName = modelName;
            this.date = date;
            this.type = type;
            this.unit = unit;
            this.currency = currency;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="BalanceCreateResponse", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class BalanceCreateResponse {
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="")]
        public EFCAO.BalanceManagement.AuthHeader AuthHeader;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public EFCAO.BalanceManagement.AdesBalanceSummary BalanceCreateResult;
        
        public BalanceCreateResponse() {
        }
        
        public BalanceCreateResponse(EFCAO.BalanceManagement.AuthHeader AuthHeader, EFCAO.BalanceManagement.AdesBalanceSummary BalanceCreateResult) {
            this.AuthHeader = AuthHeader;
            this.BalanceCreateResult = BalanceCreateResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="BalanceGetData", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class BalanceGetDataRequest {
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="")]
        public EFCAO.BalanceManagement.AuthHeader AuthHeader;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public ulong companyKey;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=1)]
        public ulong balanceKey;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=2)]
        public string[] propertyList;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=3)]
        public bool raw;
        
        public BalanceGetDataRequest() {
        }
        
        public BalanceGetDataRequest(EFCAO.BalanceManagement.AuthHeader AuthHeader, ulong companyKey, ulong balanceKey, string[] propertyList, bool raw) {
            this.AuthHeader = AuthHeader;
            this.companyKey = companyKey;
            this.balanceKey = balanceKey;
            this.propertyList = propertyList;
            this.raw = raw;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="BalanceGetDataResponse", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class BalanceGetDataResponse {
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="")]
        public EFCAO.BalanceManagement.AuthHeader AuthHeader;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public EFCAO.BalanceManagement.AdesProperty[] BalanceGetDataResult;
        
        public BalanceGetDataResponse() {
        }
        
        public BalanceGetDataResponse(EFCAO.BalanceManagement.AuthHeader AuthHeader, EFCAO.BalanceManagement.AdesProperty[] BalanceGetDataResult) {
            this.AuthHeader = AuthHeader;
            this.BalanceGetDataResult = BalanceGetDataResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="BalanceSetData", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class BalanceSetDataRequest {
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="")]
        public EFCAO.BalanceManagement.AuthHeader AuthHeader;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public ulong companyKey;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=1)]
        public ulong balanceKey;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=2)]
        public EFCAO.BalanceManagement.AdesProperty[] properties;
        
        public BalanceSetDataRequest() {
        }
        
        public BalanceSetDataRequest(EFCAO.BalanceManagement.AuthHeader AuthHeader, ulong companyKey, ulong balanceKey, EFCAO.BalanceManagement.AdesProperty[] properties) {
            this.AuthHeader = AuthHeader;
            this.companyKey = companyKey;
            this.balanceKey = balanceKey;
            this.properties = properties;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="BalanceSetDataResponse", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class BalanceSetDataResponse {
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="")]
        public EFCAO.BalanceManagement.AuthHeader AuthHeader;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public ulong BalanceSetDataResult;
        
        public BalanceSetDataResponse() {
        }
        
        public BalanceSetDataResponse(EFCAO.BalanceManagement.AuthHeader AuthHeader, ulong BalanceSetDataResult) {
            this.AuthHeader = AuthHeader;
            this.BalanceSetDataResult = BalanceSetDataResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IBalanceManagementChannel : EFCAO.BalanceManagement.IBalanceManagement, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class BalanceManagementClient : System.ServiceModel.ClientBase<EFCAO.BalanceManagement.IBalanceManagement>, EFCAO.BalanceManagement.IBalanceManagement {
        
        public BalanceManagementClient() {
        }
        
        public BalanceManagementClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public BalanceManagementClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public BalanceManagementClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public BalanceManagementClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        EFCAO.BalanceManagement.BalanceListResponse EFCAO.BalanceManagement.IBalanceManagement.BalanceList(EFCAO.BalanceManagement.BalanceListRequest request) {
            return base.Channel.BalanceList(request);
        }
        
        public EFCAO.BalanceManagement.AdesBalanceSummary[] BalanceList(ref EFCAO.BalanceManagement.AuthHeader AuthHeader, ulong companyKey, string modelName, string fromDate, string toDate, ushort type, ushort max, bool reverse) {
            EFCAO.BalanceManagement.BalanceListRequest inValue = new EFCAO.BalanceManagement.BalanceListRequest();
            inValue.AuthHeader = AuthHeader;
            inValue.companyKey = companyKey;
            inValue.modelName = modelName;
            inValue.fromDate = fromDate;
            inValue.toDate = toDate;
            inValue.type = type;
            inValue.max = max;
            inValue.reverse = reverse;
            EFCAO.BalanceManagement.BalanceListResponse retVal = ((EFCAO.BalanceManagement.IBalanceManagement)(this)).BalanceList(inValue);
            AuthHeader = retVal.AuthHeader;
            return retVal.BalanceListResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<EFCAO.BalanceManagement.BalanceListResponse> EFCAO.BalanceManagement.IBalanceManagement.BalanceListAsync(EFCAO.BalanceManagement.BalanceListRequest request) {
            return base.Channel.BalanceListAsync(request);
        }
        
        public System.Threading.Tasks.Task<EFCAO.BalanceManagement.BalanceListResponse> BalanceListAsync(EFCAO.BalanceManagement.AuthHeader AuthHeader, ulong companyKey, string modelName, string fromDate, string toDate, ushort type, ushort max, bool reverse) {
            EFCAO.BalanceManagement.BalanceListRequest inValue = new EFCAO.BalanceManagement.BalanceListRequest();
            inValue.AuthHeader = AuthHeader;
            inValue.companyKey = companyKey;
            inValue.modelName = modelName;
            inValue.fromDate = fromDate;
            inValue.toDate = toDate;
            inValue.type = type;
            inValue.max = max;
            inValue.reverse = reverse;
            return ((EFCAO.BalanceManagement.IBalanceManagement)(this)).BalanceListAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        EFCAO.BalanceManagement.BalanceCreateResponse EFCAO.BalanceManagement.IBalanceManagement.BalanceCreate(EFCAO.BalanceManagement.BalanceCreateRequest request) {
            return base.Channel.BalanceCreate(request);
        }
        
        public EFCAO.BalanceManagement.AdesBalanceSummary BalanceCreate(ref EFCAO.BalanceManagement.AuthHeader AuthHeader, ulong companyKey, string modelName, System.DateTime date, ushort type, string unit, string currency) {
            EFCAO.BalanceManagement.BalanceCreateRequest inValue = new EFCAO.BalanceManagement.BalanceCreateRequest();
            inValue.AuthHeader = AuthHeader;
            inValue.companyKey = companyKey;
            inValue.modelName = modelName;
            inValue.date = date;
            inValue.type = type;
            inValue.unit = unit;
            inValue.currency = currency;
            EFCAO.BalanceManagement.BalanceCreateResponse retVal = ((EFCAO.BalanceManagement.IBalanceManagement)(this)).BalanceCreate(inValue);
            AuthHeader = retVal.AuthHeader;
            return retVal.BalanceCreateResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<EFCAO.BalanceManagement.BalanceCreateResponse> EFCAO.BalanceManagement.IBalanceManagement.BalanceCreateAsync(EFCAO.BalanceManagement.BalanceCreateRequest request) {
            return base.Channel.BalanceCreateAsync(request);
        }
        
        public System.Threading.Tasks.Task<EFCAO.BalanceManagement.BalanceCreateResponse> BalanceCreateAsync(EFCAO.BalanceManagement.AuthHeader AuthHeader, ulong companyKey, string modelName, System.DateTime date, ushort type, string unit, string currency) {
            EFCAO.BalanceManagement.BalanceCreateRequest inValue = new EFCAO.BalanceManagement.BalanceCreateRequest();
            inValue.AuthHeader = AuthHeader;
            inValue.companyKey = companyKey;
            inValue.modelName = modelName;
            inValue.date = date;
            inValue.type = type;
            inValue.unit = unit;
            inValue.currency = currency;
            return ((EFCAO.BalanceManagement.IBalanceManagement)(this)).BalanceCreateAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        EFCAO.BalanceManagement.BalanceGetDataResponse EFCAO.BalanceManagement.IBalanceManagement.BalanceGetData(EFCAO.BalanceManagement.BalanceGetDataRequest request) {
            return base.Channel.BalanceGetData(request);
        }
        
        public EFCAO.BalanceManagement.AdesProperty[] BalanceGetData(ref EFCAO.BalanceManagement.AuthHeader AuthHeader, ulong companyKey, ulong balanceKey, string[] propertyList, bool raw) {
            EFCAO.BalanceManagement.BalanceGetDataRequest inValue = new EFCAO.BalanceManagement.BalanceGetDataRequest();
            inValue.AuthHeader = AuthHeader;
            inValue.companyKey = companyKey;
            inValue.balanceKey = balanceKey;
            inValue.propertyList = propertyList;
            inValue.raw = raw;
            EFCAO.BalanceManagement.BalanceGetDataResponse retVal = ((EFCAO.BalanceManagement.IBalanceManagement)(this)).BalanceGetData(inValue);
            AuthHeader = retVal.AuthHeader;
            return retVal.BalanceGetDataResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<EFCAO.BalanceManagement.BalanceGetDataResponse> EFCAO.BalanceManagement.IBalanceManagement.BalanceGetDataAsync(EFCAO.BalanceManagement.BalanceGetDataRequest request) {
            return base.Channel.BalanceGetDataAsync(request);
        }
        
        public System.Threading.Tasks.Task<EFCAO.BalanceManagement.BalanceGetDataResponse> BalanceGetDataAsync(EFCAO.BalanceManagement.AuthHeader AuthHeader, ulong companyKey, ulong balanceKey, string[] propertyList, bool raw) {
            EFCAO.BalanceManagement.BalanceGetDataRequest inValue = new EFCAO.BalanceManagement.BalanceGetDataRequest();
            inValue.AuthHeader = AuthHeader;
            inValue.companyKey = companyKey;
            inValue.balanceKey = balanceKey;
            inValue.propertyList = propertyList;
            inValue.raw = raw;
            return ((EFCAO.BalanceManagement.IBalanceManagement)(this)).BalanceGetDataAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        EFCAO.BalanceManagement.BalanceSetDataResponse EFCAO.BalanceManagement.IBalanceManagement.BalanceSetData(EFCAO.BalanceManagement.BalanceSetDataRequest request) {
            return base.Channel.BalanceSetData(request);
        }
        
        public ulong BalanceSetData(ref EFCAO.BalanceManagement.AuthHeader AuthHeader, ulong companyKey, ulong balanceKey, EFCAO.BalanceManagement.AdesProperty[] properties) {
            EFCAO.BalanceManagement.BalanceSetDataRequest inValue = new EFCAO.BalanceManagement.BalanceSetDataRequest();
            inValue.AuthHeader = AuthHeader;
            inValue.companyKey = companyKey;
            inValue.balanceKey = balanceKey;
            inValue.properties = properties;
            EFCAO.BalanceManagement.BalanceSetDataResponse retVal = ((EFCAO.BalanceManagement.IBalanceManagement)(this)).BalanceSetData(inValue);
            AuthHeader = retVal.AuthHeader;
            return retVal.BalanceSetDataResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<EFCAO.BalanceManagement.BalanceSetDataResponse> EFCAO.BalanceManagement.IBalanceManagement.BalanceSetDataAsync(EFCAO.BalanceManagement.BalanceSetDataRequest request) {
            return base.Channel.BalanceSetDataAsync(request);
        }
        
        public System.Threading.Tasks.Task<EFCAO.BalanceManagement.BalanceSetDataResponse> BalanceSetDataAsync(EFCAO.BalanceManagement.AuthHeader AuthHeader, ulong companyKey, ulong balanceKey, EFCAO.BalanceManagement.AdesProperty[] properties) {
            EFCAO.BalanceManagement.BalanceSetDataRequest inValue = new EFCAO.BalanceManagement.BalanceSetDataRequest();
            inValue.AuthHeader = AuthHeader;
            inValue.companyKey = companyKey;
            inValue.balanceKey = balanceKey;
            inValue.properties = properties;
            return ((EFCAO.BalanceManagement.IBalanceManagement)(this)).BalanceSetDataAsync(inValue);
        }
    }
}
