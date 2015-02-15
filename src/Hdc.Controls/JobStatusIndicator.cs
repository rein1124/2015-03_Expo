using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Hdc.Controls
{
    [TemplateVisualState(Name = JobStatusState.Uninitialized, GroupName = StateGroups.JobStatusStateGroup)]
    [TemplateVisualState(Name = JobStatusState.Preparing, GroupName = StateGroups.JobStatusStateGroup)]
    [TemplateVisualState(Name = JobStatusState.Queued, GroupName = StateGroups.JobStatusStateGroup)]
    [TemplateVisualState(Name = JobStatusState.Completed, GroupName = StateGroups.JobStatusStateGroup)]
    [TemplateVisualState(Name = JobStatusState.Running, GroupName = StateGroups.JobStatusStateGroup)]
    [TemplateVisualState(Name = JobStatusState.Suspended, GroupName = StateGroups.JobStatusStateGroup)]
    [TemplateVisualState(Name = JobStatusState.Standby, GroupName = StateGroups.JobStatusStateGroup)]
    public class JobStatusIndicator : Control
    {
        public static class StateGroups
        {
            public const string JobStatusStateGroup = "JobStatusStateGroup";
        }

        public static class JobStatusState
        {
            public const string Uninitialized = "Uninitialized";
            public const string Preparing = "Preparing";
            public const string Queued = "Queued";
            public const string Completed = "Completed";
            public const string Running = "Running";
            public const string Suspended = "Suspended";
            public const string Standby = "Standby";
        }

        static JobStatusIndicator()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof (JobStatusIndicator),
                                                     new FrameworkPropertyMetadata(typeof (JobStatusIndicator)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            UpdateJobStatusState(false);
        }

        protected void UpdateJobStatusState(bool useTransitions)
        {
            switch (Status)
            {
                case JobStatus.Completed:
                    VisualStateManager.GoToState(this, JobStatusState.Completed, useTransitions);
                    break;
                case JobStatus.Preparing:
                    VisualStateManager.GoToState(this, JobStatusState.Preparing,
                                                 useTransitions);
                    break;
                case JobStatus.Queued:
                    VisualStateManager.GoToState(this, JobStatusState.Queued,
                                                 useTransitions);
                    break;
                case JobStatus.Running:
                    VisualStateManager.GoToState(this, JobStatusState.Running, useTransitions);
                    break;
                case JobStatus.Suspended:
                    VisualStateManager.GoToState(this, JobStatusState.Suspended, useTransitions);
                    break;
                case JobStatus.Uninitialized:
                    VisualStateManager.GoToState(this, JobStatusState.Uninitialized, useTransitions);
                    break;
                case JobStatus.Standby:
                    VisualStateManager.GoToState(this, JobStatusState.Standby, useTransitions);
                    break;
            }
        }

        [Bindable(true), Category("Common Properties")]
        public JobStatus Status
        {
            get { return (JobStatus) GetValue(StatusProperty); }
            set { SetValue(StatusProperty, value); }
        }

        public static readonly DependencyProperty StatusProperty = DependencyProperty.Register(
            "Status", typeof (JobStatus), typeof (JobStatusIndicator),
            new PropertyMetadata(StatusPropertyChangedCallback));

        private static void StatusPropertyChangedCallback(DependencyObject s, DependencyPropertyChangedEventArgs e)
        {
            var me = s as JobStatusIndicator;
            if (me == null) return;

            me.UpdateJobStatusState(true);
        }
    }
}