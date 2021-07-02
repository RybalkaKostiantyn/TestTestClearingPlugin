use [uStore]

DECLARE @AssemlyName VARCHAR(250),
		@PluginClass VARCHAR(250),
		@PluginConfigAscx VARCHAR(250),
		@DataEntryAscx VARCHAR(250),
		@DataEntryConfigAscx VARCHAR(250),
		@PluginName VARCHAR(50)

SELECT 	@AssemlyName = 'TestTestClearingCommonLogic',
		@PluginClass = 'TestTestClearingCommonLogic.ClearingLogic',
		@DataEntryAscx = '~/Clearing/TestTestClearingDataEntry.ascx',
		@DataEntryConfigAscx = '~/ClearingModels/UserDataCollection/TestTestClearingDataEntryConfig.ascx',		
		@PluginConfigAscx = '~/ClearingModels/ClearingConfigControls/TestTestClearingConfig.ascx',		
		@PluginName = 'Test Test Clearing'

DECLARE @PluginDefId INT,
		@DataEntryPluginDefId INT


IF NOT EXISTS (SELECT * FROM PluginDef WHERE PluginName = @PluginName)
BEGIN

--
-- PluginDef
--

INSERT 
	INTO PluginDef ([PluginName], [Assembly],[Class], [PluginTypeId],[MallId], [ControlPath])
	VALUES (@PluginName, @AssemlyName, @PluginClass, 53, 1, NULL)

SELECT @PluginDefId = SCOPE_IDENTITY()

INSERT 
	INTO PluginDef ([PluginName], [Assembly],[Class], [PluginTypeId],[MallId], [ControlPath])
	VALUES (@PluginName, NULL, NULL, 54, 1, @DataEntryAscx)

SELECT @DataEntryPluginDefId = SCOPE_IDENTITY()


--
-- PluginDef_Culture
--

INSERT 
	INTO [PluginDef_Culture] ([PluginDefID],[CultureID], [PluginName])
	VALUES (@PluginDefId, 1, @PluginName)

INSERT 
	INTO [PluginDef_Culture] ([PluginDefID],[CultureID], [PluginName])
	VALUES (@DataEntryPluginDefId, 1, @PluginName)

--
-- PluginConfig  
--

INSERT 
	INTO PluginConfig ([PluginDefID], [ConfigurationControl])
    VALUES(@PluginDefId, @PluginConfigAscx)
    
INSERT 
	INTO PluginConfig ([PluginDefID], [ConfigurationControl])
    VALUES(@DataEntryPluginDefId, @DataEntryConfigAscx)


--
-- ClearingPluginPair
--
INSERT 
	INTO [ClearingPluginPair] ([DataCollectionPluginId],[CleringPluginId], [AllowedWithHideBillingAddress])
    VALUES (@DataEntryPluginDefId, @PluginDefId, 1)
	

END