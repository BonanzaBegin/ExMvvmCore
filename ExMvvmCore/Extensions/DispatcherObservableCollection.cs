using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ExMvvmCore.Extensions
{
    public class DispatcherObservableCollection<T> : Collection<T>, INotifyCollectionChanged
    {
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        protected override void InsertItem(int index, T item)
        {
            base.InsertItem(index, item);
            DispatcherDo(() => CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, index)));
        }

        protected override void RemoveItem(int index)
        {
            var item = this[index];
            base.RemoveItem(index);
            DispatcherDo(() => CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item, index)));

        }

        protected override void SetItem(int index, T item)
        {
            var old = this[index];
            base.SetItem(index, item);
            DispatcherDo(() => CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, item, old, index)));

        }

        protected override void ClearItems()
        {
            base.ClearItems();
            DispatcherDo(() => CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset)));

        }

        private void DispatcherDo(Action action) => Application.Current?.Dispatcher?.Invoke(action);
    }


    public class ExObservableCollection<T> : ObservableCollection<T>
    {
        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            Application.Current?.Dispatcher?.Invoke(() => base.OnCollectionChanged(e));
        }
    }
}
