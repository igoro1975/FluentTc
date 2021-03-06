using System;
using System.Collections.Generic;

namespace FluentTc.Locators
{
    public interface IBuildHavingBuilder
    {
        IBuildHavingBuilder BuildConfiguration(Action<IBuildConfigurationHavingBuilder> havingBuildConfig);
        IBuildHavingBuilder Id(int buildId);
        IBuildHavingBuilder Tags(params string[] tags);
        IBuildHavingBuilder Status(BuildStatus buildStatus);
        IBuildHavingBuilder TriggeredBy(Action<IUserHavingBuilder> buildStatus);
        IBuildHavingBuilder Personal();
        IBuildHavingBuilder NotPersonal();
        IBuildHavingBuilder Cancelled();
        IBuildHavingBuilder NotCancelled();
        IBuildHavingBuilder Running();
        IBuildHavingBuilder NotRunning();
        IBuildHavingBuilder Pinned();
        IBuildHavingBuilder NotPinned();
        IBuildHavingBuilder Branch(Action<IBranchHavingBuilder> branchHavingBuilder);
        IBuildHavingBuilder AgentName(string agentName);
        IBuildHavingBuilder SinceBuild(Action<IBuildHavingBuilder> buildHavingBuilder);
        IBuildHavingBuilder SinceDate(DateTime dateTime);
        IBuildHavingBuilder Project(Action<IBuildProjectHavingBuilder> projectHavingBuilder);
        string GetLocator();
    }

    public class BuildHavingBuilder : IBuildHavingBuilder
    {
        private const string DateFormat = "yyyyMMddTHHmmsszz00";

        private readonly List<string> m_Having = new List<string>();
        private readonly IBuildConfigurationHavingBuilderFactory m_BuildConfigurationHavingBuilderFactory;
        private readonly IBuildHavingBuilderFactory m_BuildHavingBuilderFactory;
        private readonly IUserHavingBuilderFactory m_UserHavingBuilderFactory;
        private readonly IBranchHavingBuilderFactory m_BranchHavingBuilderFactory;
        private readonly IBuildProjectHavingBuilderFactory m_BuildProjectHavingBuilderFactory;

        public BuildHavingBuilder(IBuildConfigurationHavingBuilderFactory buildConfigurationHavingBuilderFactory, IBuildHavingBuilderFactory buildHavingBuilderFactory, IUserHavingBuilderFactory userHavingBuilderFactory, IBranchHavingBuilderFactory branchHavingBuilderFactory, IBuildProjectHavingBuilderFactory buildProjectHavingBuilderFactory)
        {
            m_BuildConfigurationHavingBuilderFactory = buildConfigurationHavingBuilderFactory;
            m_BuildHavingBuilderFactory = buildHavingBuilderFactory;
            m_UserHavingBuilderFactory = userHavingBuilderFactory;
            m_BranchHavingBuilderFactory = branchHavingBuilderFactory;
            m_BuildProjectHavingBuilderFactory = buildProjectHavingBuilderFactory;
        }

        public IBuildHavingBuilder BuildConfiguration(Action<IBuildConfigurationHavingBuilder> havingBuildConfig)
        {
            var buildConfigurationHavingBuilder = m_BuildConfigurationHavingBuilderFactory.CreateBuildConfigurationHavingBuilder();
            havingBuildConfig.Invoke(buildConfigurationHavingBuilder);
            m_Having.Add("buildType:" + buildConfigurationHavingBuilder.GetLocator());
            return this;
        }

        public IBuildHavingBuilder Id(int buildId)
        {
            m_Having.Add("id:" + buildId);
            return this;
        }

        public IBuildHavingBuilder Tags(params string[] tags)
        {
            m_Having.Add("tags:" + string.Join(",", tags));
            return this;
        }

        public IBuildHavingBuilder Status(BuildStatus buildStatus)
        {
            m_Having.Add("status:" + buildStatus.ToString().ToUpper());
            return this;
        }

        public IBuildHavingBuilder TriggeredBy(Action<IUserHavingBuilder> buildStatus)
        {
            var userHavingBuilder = m_UserHavingBuilderFactory.CreateUserHavingBuilder();
            buildStatus(userHavingBuilder);
            m_Having.Add("user:" + userHavingBuilder.GetLocator());
            return this;
        }

        public IBuildHavingBuilder Personal()
        {
            m_Having.Add("personal:" + bool.TrueString);
            return this;
        }

        public IBuildHavingBuilder NotPersonal()
        {
            m_Having.Add("personal:" + bool.FalseString);
            return this;
        }

        public IBuildHavingBuilder Cancelled()
        {
            m_Having.Add("cancelled:" + bool.TrueString);
            return this;
        }

        public IBuildHavingBuilder NotCancelled()
        {
            m_Having.Add("cancelled:" + bool.FalseString);
            return this;
        }

        public IBuildHavingBuilder Running()
        {
            m_Having.Add("running:" + bool.TrueString);
            return this;
        }

        public IBuildHavingBuilder NotRunning()
        {
            m_Having.Add("running:" + bool.FalseString);
            return this;
        }

        public IBuildHavingBuilder Pinned()
        {
            m_Having.Add("pinned:" + bool.TrueString);
            return this;
        }

        public IBuildHavingBuilder NotPinned()
        {
            m_Having.Add("pinned:" + bool.FalseString);
            return this;
        }

        public IBuildHavingBuilder Branch(Action<IBranchHavingBuilder> branchHavingBuilder)
        {
            var havingBuilder = m_BranchHavingBuilderFactory.CreateBranchHavingBuilder();
            branchHavingBuilder(havingBuilder);
            m_Having.Add("branch:" + havingBuilder.GetLocator());
            return this;
        }

        public IBuildHavingBuilder AgentName(string agentName)
        {
            m_Having.Add("agentName:" + agentName);
            return this;
        }

        public IBuildHavingBuilder SinceBuild(Action<IBuildHavingBuilder> buildHavingBuilder)
        {
            var havingBuilder = m_BuildHavingBuilderFactory.CreateBuildHavingBuilder();
            buildHavingBuilder(havingBuilder);
            m_Having.Add("sinceBuild:" + havingBuilder.GetLocator());
            return this;
        }

        public IBuildHavingBuilder SinceDate(DateTime dateTime)
        {
            m_Having.Add("sinceDate:" + dateTime.ToString(DateFormat));
            return this;
        }

        public IBuildHavingBuilder Project(Action<IBuildProjectHavingBuilder> projectHavingBuilder)
        {
            var buildProjectHavingBuilder = m_BuildProjectHavingBuilderFactory.CreateBuildProjectHavingBuilder();
            projectHavingBuilder(buildProjectHavingBuilder);
            m_Having.Add("project:" + buildProjectHavingBuilder.GetLocator());
            return this;
        }

        string IBuildHavingBuilder.GetLocator()
        {
            return string.Join(",", m_Having);
        }
    }
}