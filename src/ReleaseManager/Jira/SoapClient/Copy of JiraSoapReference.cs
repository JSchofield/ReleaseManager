namespace ReleaseManager.Jira.SoapClient
{
    public partial class RemoteIssue : AbstractRemoteEntity
    {

        private RemoteVersion[] affectsVersionsField;

        private string assigneeField;

        private string[] attachmentNamesField;

        private RemoteComponent[] componentsField;

        private System.Nullable<System.DateTime> createdField;

        private RemoteCustomFieldValue[] customFieldValuesField;

        private string descriptionField;

        private System.Nullable<System.DateTime> duedateField;

        private string environmentField;

        private RemoteVersion[] fixVersionsField;

        private string keyField;

        private string priorityField;

        private string projectField;

        private string reporterField;

        private string resolutionField;

        private string statusField;

        private string summaryField;

        private string typeField;

        private System.Nullable<System.DateTime> updatedField;

        private System.Nullable<long> votesField;

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public RemoteVersion[] affectsVersions
        {
            get
            {
                return this.affectsVersionsField;
            }
            set
            {
                this.affectsVersionsField = value;
                this.RaisePropertyChanged("affectsVersions");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string assignee
        {
            get
            {
                return this.assigneeField;
            }
            set
            {
                this.assigneeField = value;
                this.RaisePropertyChanged("assignee");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string[] attachmentNames
        {
            get
            {
                return this.attachmentNamesField;
            }
            set
            {
                this.attachmentNamesField = value;
                this.RaisePropertyChanged("attachmentNames");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public RemoteComponent[] components
        {
            get
            {
                return this.componentsField;
            }
            set
            {
                this.componentsField = value;
                this.RaisePropertyChanged("components");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public System.Nullable<System.DateTime> created
        {
            get
            {
                return this.createdField;
            }
            set
            {
                this.createdField = value;
                this.RaisePropertyChanged("created");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public RemoteCustomFieldValue[] customFieldValues
        {
            get
            {
                return this.customFieldValuesField;
            }
            set
            {
                this.customFieldValuesField = value;
                this.RaisePropertyChanged("customFieldValues");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
                this.RaisePropertyChanged("description");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public System.Nullable<System.DateTime> duedate
        {
            get
            {
                return this.duedateField;
            }
            set
            {
                this.duedateField = value;
                this.RaisePropertyChanged("duedate");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string environment
        {
            get
            {
                return this.environmentField;
            }
            set
            {
                this.environmentField = value;
                this.RaisePropertyChanged("environment");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public RemoteVersion[] fixVersions
        {
            get
            {
                return this.fixVersionsField;
            }
            set
            {
                this.fixVersionsField = value;
                this.RaisePropertyChanged("fixVersions");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string key
        {
            get
            {
                return this.keyField;
            }
            set
            {
                this.keyField = value;
                this.RaisePropertyChanged("key");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string priority
        {
            get
            {
                return this.priorityField;
            }
            set
            {
                this.priorityField = value;
                this.RaisePropertyChanged("priority");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string project
        {
            get
            {
                return this.projectField;
            }
            set
            {
                this.projectField = value;
                this.RaisePropertyChanged("project");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string reporter
        {
            get
            {
                return this.reporterField;
            }
            set
            {
                this.reporterField = value;
                this.RaisePropertyChanged("reporter");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string resolution
        {
            get
            {
                return this.resolutionField;
            }
            set
            {
                this.resolutionField = value;
                this.RaisePropertyChanged("resolution");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string status
        {
            get
            {
                return this.statusField;
            }
            set
            {
                this.statusField = value;
                this.RaisePropertyChanged("status");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string summary
        {
            get
            {
                return this.summaryField;
            }
            set
            {
                this.summaryField = value;
                this.RaisePropertyChanged("summary");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public string type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
                this.RaisePropertyChanged("type");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public System.Nullable<System.DateTime> updated
        {
            get
            {
                return this.updatedField;
            }
            set
            {
                this.updatedField = value;
                this.RaisePropertyChanged("updated");
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable = true)]
        public System.Nullable<long> votes
        {
            get
            {
                return this.votesField;
            }
            set
            {
                this.votesField = value;
                this.RaisePropertyChanged("votes");
            }
        }
    }

    public partial class JiraSoapServiceClient : System.ServiceModel.ClientBase<JiraSoapService>, JiraSoapService
    {
        public JiraSoapServiceClient()
        {
        }

        public JiraSoapServiceClient(string endpointConfigurationName) :
            base(endpointConfigurationName)
        {
        }

        public JiraSoapServiceClient(string endpointConfigurationName, string remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        public JiraSoapServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        public JiraSoapServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
            base(binding, remoteAddress)
        {
        }

        public string login(string in0, string in1)
        {
            loginRequest inValue = new loginRequest();
            inValue.in0 = in0;
            inValue.in1 = in1;
            loginResponse retVal = base.Channel.login(inValue);
            return retVal.loginReturn;
        }

        public RemoteIssue getIssue(string in0, string in1)
        {
            getIssueRequest inValue = new getIssueRequest();
            inValue.in0 = in0;
            inValue.in1 = in1;
            getIssueResponse retVal = base.Channel.getIssue(inValue);
            return retVal.getIssueReturn;
        }

        public RemoteStatus[] getStatuses(string in0)
        {
            getStatusesRequest inValue = new getStatusesRequest();
            inValue.in0 = in0;
            getStatusesResponse retVal = base.Channel.getStatuses(inValue);
            return retVal.getStatusesReturn;
        }       
    }
}
