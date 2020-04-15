﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using PersonalRemoteManager;
using PRM.Core.Model;
using PRM.Core.Protocol;
using PRM.View;
using PRM.ViewModel;
using Shawn.Ulits;

namespace PRM
{
    public partial class MainWindow : Window
    {
        public readonly VmMain VmMain;
        public MainWindow()
        {
            InitializeComponent();
            VmMain = new VmMain();
            this.DataContext = VmMain;
            Title = SystemConfig.AppName;


            this.Width = SystemConfig.GetInstance().Locality.MainWindowWidth;
            this.Height = SystemConfig.GetInstance().Locality.MainWindowHeight;




            BtnClose.Click += (sender, args) =>
            {
                HideMe();
                App.TaskTrayIcon?.ShowBalloonTip(1000);
            };
            BtnMaximize.Click += (sender, args) => this.WindowState = (this.WindowState == WindowState.Normal) ? WindowState.Maximized : WindowState.Normal;
            BtnMinimize.Click += (sender, args) => { this.WindowState = WindowState.Minimized; };
        }

        /// <summary>
        /// DragMove
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void System_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }


        private void BtnSetting_OnClick(object sender, RoutedEventArgs e)
        {
            PopupSettingMenu.IsOpen = true;
        }


        public void ActivateMe()
        {
            Dispatcher.Invoke(() =>
            {
                this.Visibility = Visibility.Visible;
                this.ShowInTaskbar = true;
                this.Activate();
            });
        }
        public void HideMe()
        {
            Dispatcher.Invoke(() =>
            {
                this.ShowInTaskbar = false;
                this.Visibility = Visibility.Hidden;
            });
        }

        private void GridAbout_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            VmMain.CmdGoAboutPage.Execute();
        }



        private void CommandFocusFilter_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            TbFilter.Focus();
        }

        private void TbFilter_OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                (sender as TextBox).Text = "";
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
            {
                SystemConfig.GetInstance().Locality.MainWindowHeight = this.Height;
                SystemConfig.GetInstance().Locality.MainWindowWidth = this.Width;
                SystemConfig.GetInstance().Locality.Save();
            }
        }

        private void ButtonExit_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
