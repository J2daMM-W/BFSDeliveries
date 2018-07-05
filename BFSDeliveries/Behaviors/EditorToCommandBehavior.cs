using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace BFSDeliveries
{
    public class EditorToCommandBehavior : BehaviorBase<Editor>
    {
        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(EditorToCommandBehavior), null);

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        protected override void OnAttachedTo(Editor bindable)
        {
            base.OnAttachedTo(bindable);

            if (bindable.BindingContext != null)
                BindingContext = bindable.BindingContext;

            bindable.BindingContextChanged += Bindable_BindingContextChanged;
            bindable.Focused += Bindable_Focused;
            //bindable.TextChanged += Bindable_TextChanged;
        }

        protected override void OnDetachingFrom(Editor bindable)
        {
            base.OnDetachingFrom(bindable);

            bindable.BindingContextChanged -= Bindable_BindingContextChanged;
            bindable.Focused -= Bindable_Focused;
            //bindable.TextChanged -= Bindable_TextChanged;
        }

        void Bindable_Focused(object sender, FocusEventArgs e)
        {
            Editor editor = sender as Editor;

            Command?.Execute(editor);
        }

        //void Bindable_UnFocused(object sender, FocusEventArgs e)
        //{
        //    throw new NotImplementedException();
        //}

        //void Bindable_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    Editor editor = sender as Editor;
        //    string text ;

        //    if(e.OldTextValue != null)
        //    {
        //        text = string.Join(",", e.NewTextValue);
        //    }
        //    if (Command == null)
        //        return;
            
        //    Command?.Execute(e.NewTextValue);
        //}

        void Bindable_BindingContextChanged(object sender, EventArgs e)
        {
            base.OnBindingContextChanged();

            if (!(sender is BindableObject bindable))
                return;

            BindingContext = bindable.BindingContext;
        }
    }
}
