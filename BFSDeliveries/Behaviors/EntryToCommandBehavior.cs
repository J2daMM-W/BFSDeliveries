using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace BFSDeliveries
{
    public class EntryToCommandBehavior : BehaviorBase<ExtendedEntry>
    {
        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(EntryToCommandBehavior), null);

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        protected override void OnAttachedTo(ExtendedEntry bindable)
        {
            base.OnAttachedTo(bindable);

            if (bindable.BindingContext != null)
                BindingContext = bindable.BindingContext;

            bindable.BindingContextChanged += Bindable_BindingContextChanged;

            bindable.TextChanged += Bindable_TextChanged;
        }

        protected override void OnDetachingFrom(ExtendedEntry bindable)
        {
            base.OnDetachingFrom(bindable);

            bindable.BindingContextChanged -= Bindable_BindingContextChanged;

            bindable.TextChanged -= Bindable_TextChanged;
        }

        void Bindable_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Command == null)
                return;
            
            Command?.Execute(e.NewTextValue);
        }

        void Bindable_BindingContextChanged(object sender, EventArgs e)
        {
            base.OnBindingContextChanged();

            if (!(sender is BindableObject bindable))
                return;

            BindingContext = bindable.BindingContext;
        }
    }
}
