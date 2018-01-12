namespace UIC.Communication.M2mgo.ProjectAgent.WebApi.datamodel.Project
{
    public class SgetProjectDataPointTask
    {
        public SgetProjectDataPointTask(SgetProjectDataPointTaskInfo info, long? pollIntervall, SgetCloudReportingCondition cloudCondition, SgetCloudViewMetadata viewMetaData, string description) {
            Description = description;
            ViewMetaData = viewMetaData;
            Info = info;
            PollIntervall = pollIntervall ?? (10 * 1000);
            CloudCondition = cloudCondition;
        }

        public long PollIntervall { get; private set; }
        public SgetCloudReportingCondition CloudCondition { get; private set; }
        public SgetProjectDataPointTaskInfo Info { get; private set; }
        public SgetCloudViewMetadata ViewMetaData { get; private set; }
        public string Description { get; private set; }

        public override string ToString()
        {
            return Info.ToString();
        }
    }
}