using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using Todo.Data;
using Todo.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Todo.Views
{
    public partial class TodoListPage : ContentPage
    {
        public TodoListPage()
        {
            InitializeComponent();

        }


        // Database connection


        SQLiteAsyncConnection dataBase;
        protected async override void OnAppearing()
        {
            base.OnAppearing();

            TodoItemDatabase database = await TodoItemDatabase.Instance;
            listView.ItemsSource = await database.GetItemsAsync();
            var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var databasePath = Path.Combine(basePath, "SQLite.db3");

            dataBase = new SQLiteAsyncConnection(databasePath);
            await dataBase.CreateTableAsync(typeof(TodoItem));
            //var product = await dataBase.Table<TodoItem>().ToListAsync();
            // var personList = await database.GetItemsAsync();
            //if (personList != null)
            //{
            //   listView.ItemsSource = personList;
            // }

           



        }






        //to Add item page
        async void OnItemAdded(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TodoItemPage
            {
                BindingContext = new TodoItem()
            });
        }
        
        async void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new TodoItemPage
                {
                    BindingContext = e.SelectedItem as TodoItem
                });
            }
        }





        //Button to fetch and show jason data

        async void Button_Clicked(object sender, EventArgs e)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync("https://jsonplaceholder.typicode.com/users");
            var product = JsonConvert.DeserializeObject<List<TodoItem>>(response);
            // product = await dataBase.Table<TodoItem>().ToListAsync();
            await dataBase.InsertAllAsync(product);
            //var products = await dataBase.Table<TodoItem>().ToListAsync();

            //foreach(var item in product)
            //{
              //  await dataBase.InsertAsync(item);
            //}

            jason.ItemsSource = product;

        }
    }
}