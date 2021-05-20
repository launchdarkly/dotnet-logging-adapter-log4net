using System.Reflection;

namespace LaunchDarkly.Logging
{
    /// <summary>
    /// Provides integration between the LaunchDarkly SDK's logging framework and
    /// the <c>log4net</c> framework.
    /// </summary>
    public static class LdLog4net
    {
        /// <summary>
        /// Returns an adapter for directing <c>LaunchDarkly.Logging</c> output to
        /// <c>log4net</c>.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Using this adapter will cause <c>LaunchDarkly.Logging</c> to delegate each
        /// logger it creates to a corresponding logger created with
        /// <c>log4net.LogManager.GetLogger</c>. What happens to the log output then is
        /// entirely determined by the <c>log4net</c> configuration; there are no
        /// configuration methods on the <c>Adapter</c> itself. The logger names that are
        /// used within the <c>LaunchDarkly.Logging</c> framework are passed along as
        /// logger names to <c>log4net</c>, so they can be used in filtering rules, etc.
        /// </para>
        /// <para>
        /// The example code below shows how to configure the LaunchDarkly SDK to
        /// use <c>log4net</c>.
        /// </para>
        /// </remarks>
        /// <example>
        /// <code>
        ///     using LaunchDarkly.Logging;
        ///     using LaunchDarkly.Sdk.Server;
        ///
        ///     var config = Configuration.Builder("my-sdk-key")
        ///         .Logging(LdLog4net.Adapter)
        ///         .Build();
        ///     var client = new LdClient(config);
        /// </code>
        /// </example>
        /// <returns>an <c>ILogAdapter</c> that delegates to <c>log4net</c></returns>
        public static ILogAdapter Adapter => Log4netAdapter.Instance;
    }

    internal sealed class Log4netAdapter : ILogAdapter
    {
        internal static readonly Log4netAdapter Instance = new Log4netAdapter();

        public IChannel NewChannel(string name) => new Log4netChannel(name);
    }

    internal sealed class Log4netChannel : IChannel
    {
        private readonly log4net.ILog _log;

        internal Log4netChannel(string name)
        {
            _log = log4net.LogManager.GetLogger(Assembly.GetCallingAssembly(), name);
        }

        public bool IsEnabled(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Debug:
                    return _log.IsDebugEnabled;
                case LogLevel.Info:
                    return _log.IsInfoEnabled;
                case LogLevel.Warn:
                    return _log.IsWarnEnabled;
                case LogLevel.Error:
                    return _log.IsErrorEnabled;
                default:
                    return false;
            }
        }

        public void Log(LogLevel level, object message)
        {
            switch (level)
            {
                case LogLevel.Debug:
                    _log.Debug(message);
                    break;
                case LogLevel.Info:
                    _log.Info(message);
                    break;
                case LogLevel.Warn:
                    _log.Warn(message);
                    break;
                case LogLevel.Error:
                    _log.Error(message);
                    break;
            }
        }

        public void Log(LogLevel level, string format, object param)
        {
            switch (level)
            {
                case LogLevel.Debug:
                    _log.DebugFormat(format, param);
                    break;
                case LogLevel.Info:
                    _log.InfoFormat(format, param);
                    break;
                case LogLevel.Warn:
                    _log.WarnFormat(format, param);
                    break;
                case LogLevel.Error:
                    _log.ErrorFormat(format, param);
                    break;
            }
        }

        public void Log(LogLevel level, string format, object param1, object param2)
        {
            switch (level)
            {
                case LogLevel.Debug:
                    _log.DebugFormat(format, param1, param2);
                    break;
                case LogLevel.Info:
                    _log.InfoFormat(format, param1, param2);
                    break;
                case LogLevel.Warn:
                    _log.WarnFormat(format, param1, param2);
                    break;
                case LogLevel.Error:
                    _log.ErrorFormat(format, param1, param2);
                    break;
            }
        }

        public void Log(LogLevel level, string format, params object[] allParams)
        {
            switch (level)
            {
                case LogLevel.Debug:
                    _log.DebugFormat(format, allParams);
                    break;
                case LogLevel.Info:
                    _log.InfoFormat(format, allParams);
                    break;
                case LogLevel.Warn:
                    _log.WarnFormat(format, allParams);
                    break;
                case LogLevel.Error:
                    _log.ErrorFormat(format, allParams);
                    break;
            }
        }
    }
}
