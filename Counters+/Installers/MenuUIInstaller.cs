﻿using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.MenuButtons;
using CountersPlus.UI;
using CountersPlus.UI.FlowCoordinators;
using CountersPlus.UI.SettingGroups;
using CountersPlus.UI.ViewControllers;
using CountersPlus.UI.ViewControllers.Editing;
using CountersPlus.UI.ViewControllers.HUDs;
using CountersPlus.Utils;
using HMUI;
using SiraUtil.Zenject;
using System.Linq;
using Zenject;

namespace CountersPlus.Installers
{
    public class MenuUIInstaller : MonoInstaller
    {
        private CountersPlusSettingsFlowCoordinator flowCoordinator;

        public override void InstallBindings()
        {
            // Using Zenject for UI lets goooooooooooo
            Container.BindInstance(Container).WhenInjectedInto<CountersPlusSettingsFlowCoordinator>();

            // CanvasUtility for UI
            Container.Bind<CanvasUtility>().AsSingle().NonLazy();
            Container.Bind<MockCounter>().AsSingle().NonLazy();

            BindSettingsGroup<MainSettingsGroup>();
            BindSettingsGroup<CountersSettingsGroup>();
            BindSettingsGroup<HUDsSettingsGroup>();

            BindViewController<CountersPlusCreditsViewController>();
            BindViewController<CountersPlusMainScreenNavigationController>();
            BindViewController<CountersPlusBlankViewController>();
            BindViewController<CountersPlusSettingSectionSelectionViewController>();
            BindViewController<CountersPlusHorizontalSettingsListViewController>();
            BindViewController<CountersPlusCounterEditViewController>();
            BindViewController<CountersPlusUnimplementedViewController>(); // TODO remove for Counters+ 2.0 Release
            BindViewController<CountersPlusMainSettingsEditViewController>();
            BindViewController<CountersPlusHUDListViewController>();

            flowCoordinator = BeatSaberUI.CreateFlowCoordinator<CountersPlusSettingsFlowCoordinator>();
            Container.InjectSpecialInstance<CountersPlusSettingsFlowCoordinator>(flowCoordinator);

            MenuButton button = new MenuButton("Counters+", "Configure Counters+ settings.", OnClick);
            MenuButtons.instance.RegisterButton(button);
        }

        private void BindViewController<T>() where T : ViewController
        {
            T view = BeatSaberUI.CreateViewController<T>();
            Container.InjectSpecialInstance<T>(view);
        }

        private void BindSettingsGroup<T>() where T : SettingsGroup
        {
            Container.Bind<SettingsGroup>().To<T>().AsCached().NonLazy();
        }

        private void OnClick()
        {
            BeatSaberUI.MainFlowCoordinator.PresentFlowCoordinator(flowCoordinator);
        }
    }
}
