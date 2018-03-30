using System;
using System.Windows.Input;
using DisplayActionSheet.Models;
using Xamarin.Forms;

namespace DisplayActionSheet
{
    public class ActionSheet : BehaviorBase<Page>
    {
        #region Parameters
        public static BindableProperty ParametersProperty =
            BindableProperty.CreateAttached(
            "Parameters",
            typeof(ActionSheetParameters),
            typeof(ActionSheet),
            default(ActionSheetParameters));

        public static ActionSheetParameters GetParameters(BindableObject bindable)
        {
            return (ActionSheetParameters)bindable.GetValue(ParametersProperty);
        }

        public static void SetParameters(BindableObject bindable, ActionSheetParameters value)
        {
            bindable.SetValue(ParametersProperty, value);
        }
        #endregion

        #region Result
        public static BindableProperty ResultProperty =
            BindableProperty.CreateAttached(
            "Result",
            typeof(string),
            typeof(ActionSheet),
            default(string),
            defaultBindingMode: BindingMode.TwoWay);

        public static string GetResult(BindableObject bindable)
        {
            return (string)bindable.GetValue(ResultProperty);
        }

        public static void SetResult(BindableObject bindable, string value)
        {
            bindable.SetValue(ResultProperty, value);
        }
        #endregion

        #region Command
        public static BindableProperty CommandProperty =
            BindableProperty.CreateAttached(
            "Command",
            typeof(ICommand),
            typeof(ActionSheet),
            null);

        public static ICommand GetCommand(BindableObject bindable)
        {
            return (ICommand)bindable.GetValue(CommandProperty);
        }

        public static void SetCommand(BindableObject bindable, string value)
        {
            bindable.SetValue(CommandProperty, value);
        }
        #endregion

        #region IsVisible
        public static BindableProperty IsVisibleProperty =
            BindableProperty.CreateAttached(
            "IsVisible",
            typeof(bool),
            typeof(ActionSheet),
            default(bool),
            propertyChanged: OnShowDialog,
            defaultBindingMode: BindingMode.TwoWay);

        public static bool GetIsVisible(BindableObject bindable)
        {
            return (bool)bindable.GetValue(IsVisibleProperty);
        }

        public static void SetIsVisible(BindableObject bindable, bool value)
        {
            bindable.SetValue(IsVisibleProperty, value);
        }
        #endregion

        private static async void OnShowDialog(BindableObject bindable, object oldValue, object newValue)
        {
            bool showAlert = (bool)newValue;
            if (showAlert)
            {
                var page = VisualTreeHelper.GetParent<Page>((Element)bindable);

                ActionSheetParameters args = GetParameters(bindable);

                if (page != null && args != null)
                {
                    string result = await page.DisplayActionSheet(args.Title, args.Cancel, args.Destruction, args.Buttons);

                    SetResult(bindable, result); // pass result back to binding

                    // pas result back to viewmodel using command
                    ICommand command = GetCommand(bindable);
                    if (result != null && command != null)
                    {
                        command.Execute(result);
                    }

                    SetIsVisible(bindable, false); // reset the dialog
                }
            }
        }
    }

    public class ActionSheetParameters
    {
        /// <summary>
        /// Action sheet title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Cancel button title
        /// </summary>
        public string Cancel { get; set; }

        /// <summary>
        /// Destructive action title
        /// </summary>
        public string Destruction { get; set; }

        /// <summary>
        /// List of Buttons
        /// </summary>
        public string[] Buttons { get; set; }
    }
}
