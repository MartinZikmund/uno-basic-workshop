using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Octokit;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Windows.Networking.Connectivity;
using Windows.UI.Popups;
using System;

namespace HelloUnoNdc.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private string _searchText;
        private ObservableCollection<User> _users;
        private readonly GitHubClient _client;

        public MainViewModel()
        {
            _client = new GitHubClient(new ProductHeaderValue("HelloUnoNDC"));
        }

        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<User> Users
        {
            get => _users;
            set
            {
                _users = value;
                OnPropertyChanged();
            }
        }

        public ICommand SearchCommand => new AsyncRelayCommand(async () =>
        {
            if (!IsConnected())
            {
                var dialog = new MessageDialog("You are not connected, try again later.");
                await dialog.ShowAsync();
                return;
            }
            var users = await _client.Search.SearchUsers(new SearchUsersRequest(SearchText));
            Users = new ObservableCollection<User>(users.Items);
        });

        private bool IsConnected()
        {
            var profile = NetworkInformation.GetInternetConnectionProfile();
            var connectivity = profile.GetNetworkConnectivityLevel();
            return connectivity == NetworkConnectivityLevel.InternetAccess;
        }
    }
}
