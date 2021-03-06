using Autofac;
using Lykke.Common.MsSql;
using MAVN.Service.CrossChainTransfers.Domain.Repositories;
using MAVN.Service.CrossChainTransfers.MsSqlRepositories;
using MAVN.Service.CrossChainTransfers.MsSqlRepositories.Repositories;
using MAVN.Service.CrossChainTransfers.Settings;
using Lykke.SettingsReader;

namespace MAVN.Service.CrossChainTransfers.Modules
{
    public class DataLayerModule : Module
    {
        private readonly DbSettings _dbSettings;

        public DataLayerModule(IReloadingManager<AppSettings> appSettings)
        {
            _dbSettings = appSettings.CurrentValue.CrossChainTransfersService.Db;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterMsSql(
                _dbSettings.DataConnString,
                connString => new CrossChainTransfersContext(connString, false),
                dbConn => new CrossChainTransfersContext(dbConn));

            builder.RegisterType<DeduplicationLogRepository>()
                .As<IDeduplicationLogRepository>()
                .SingleInstance();
        }
    }
}
