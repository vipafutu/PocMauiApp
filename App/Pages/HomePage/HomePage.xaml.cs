namespace PocMauiApp.Pages.HomePage;

using System;
using PocMauiApp.Services;

public partial class HomePage : ContentPage
{
    private readonly INavigationService _navigationService;

    public HomePage(HomePageViewModel homePageViewModel, INavigationService navigationService)
    {
        InitializeComponent();

        _navigationService = navigationService;
        homePageViewModel.SelectedCategoryChanged += HomePageViewModel_SelectedCategoryChanged;
        BindingContext = homePageViewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        var viewModel = BindingContext as HomePageViewModel;
        if (viewModel != null)
        {
            viewModel.SelectedCategory = null;
        }
    }

    private void HomePageViewModel_SelectedCategoryChanged(object? sender, EventArgs e)
    {
        if (sender == null) return;

        var selectedCategory = ((HomePageViewModel)sender).SelectedCategory;
        if (selectedCategory != null)
        {
            _navigationService.ToProductListPage(Navigation, selectedCategory.Id);
        }
    }
}
