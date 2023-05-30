using DocentesApp.Services;
using DocentesApp.ViewModels;

namespace DocentesApp.Views.Login;

public partial class LoginView : ContentPage
{
	public LoginView(LoginService login)
	{
		InitializeComponent();
		this.BindingContext = new LoginViewModel(login);
	}
}