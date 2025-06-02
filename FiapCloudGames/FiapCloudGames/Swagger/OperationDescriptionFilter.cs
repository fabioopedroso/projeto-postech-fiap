using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace FiapCloudGamesApi.Swagger
{
    public class OperationDescriptionFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var actionName = context.MethodInfo.Name;

            switch (actionName)
            {
                //Administrator
                case "CreateUser":
                    operation.Summary = "Create a new user";
                    operation.Description = "Creates a new user in the system with the provided details.";
                    break;
                case "GetUsers":
                    operation.Summary = "Retrieve all users";
                    operation.Description = "Returns a list of all registered users.";
                    break;
                case "GetUserById":
                    operation.Summary = "Retrieve user by ID";
                    operation.Description = "Returns detailed information about a user based on the provided ID.";
                    break;
                case "PromoteUser":
                    operation.Summary = "Promote user";
                    operation.Description = "Promotes a user to an administrator role.";
                    break;
                case "DemoteUser":
                    operation.Summary = "Demote user";
                    operation.Description = "Demotes an administrator back to a regular user.";
                    break;
                case "SetUserActiveStatus":
                    operation.Summary = "Set user active status";
                    operation.Description = "Activates or deactivates a user account.";
                    break;

                //Auth
                case "Login":
                    operation.Summary = "User login";
                    operation.Description = "Authenticates a user and returns a JWT token.";
                    break;

                //Cart
                case "GetCartSummary":
                    operation.Summary = "Get cart summary";
                    operation.Description = "Returns the current cart summary with items and total price.";
                    break;
                case "AddGame":
                    operation.Summary = "Add game to cart";
                    operation.Description = "Adds a specific game to the cart.";
                    break;
                case "RemoveGame":
                    operation.Summary = "Remove game from cart";
                    operation.Description = "Removes a specific game from the cart.";
                    break;
                case "ClearCart":
                    operation.Summary = "Clear cart";
                    operation.Description = "Removes all games from the cart.";
                    break;

                //Checkout
                case "CheckoutCart":
                    operation.Summary = "Process checkout";
                    operation.Description = "Processes the purchase of games in the cart.";
                    break;

                //Game
                case "GetAll":
                    operation.Summary = "Retrieve all games";
                    operation.Description = "Returns a list of all available games.";
                    break;
                case "Add":
                    operation.Summary = "Create a new game";
                    operation.Description = "Adds a new game to the catalog.";
                    break;
                case "GetById":
                    operation.Summary = "Retrieve game by ID";
                    operation.Description = "Returns details of a specific game by ID.";
                    break;
                case "Update":
                    operation.Summary = "Update game";
                    operation.Description = "Updates the details of an existing game.";
                    break;
                case "SetActiveStatus":
                    operation.Summary = "Set game active status";
                    operation.Description = "Activates or deactivates a game.";
                    break;
                case "SetPrice":
                    operation.Summary = "Set game price";
                    operation.Description = "Updates the price of a game.";
                    break;

                //Library
                case "ListLibraryGames":
                    operation.Summary = "List library games";
                    operation.Description = "Returns a list of all games owned by the authenticated user.";
                    break;

                //Sale
                case "CreateSale":
                    operation.Summary = "Create a new sale";
                    operation.Description = "Creates a new sale or discount event for games.";
                    break;
                case "UpdateSale":
                    operation.Summary = "Update sale";
                    operation.Description = "Updates an existing sale's details.";
                    break;
                case "GetAllSales":
                    operation.Summary = "Retrieve all sales";
                    operation.Description = "Returns a list of all active sales.";
                    break;
                case "GetSaleById":
                    operation.Summary = "Retrieve sale by ID";
                    operation.Description = "Returns details of a specific sale.";
                    break;
                case "AddGameToSale":
                    operation.Summary = "Add game to sale";
                    operation.Description = "Adds a game to a specific sale.";
                    break;
                case "RemoveGameFromSale":
                    operation.Summary = "Remove game from sale";
                    operation.Description = "Removes a game from a specific sale.";
                    break;

                //User
                case "Register":
                    operation.Summary = "Register new user";
                    operation.Description = "Registers a new user in the platform.";
                    break;
                case "ChangePassword":
                    operation.Summary = "Change user password";
                    operation.Description = "Changes the password of the authenticated user.";
                    break;
                case "DeleteUser":
                    operation.Summary = "Delete user account";
                    operation.Description = "Deletes the user account permanently.";
                    break;
            }
        }
    }
}
