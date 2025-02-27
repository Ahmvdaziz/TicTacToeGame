# Tic-Tac-Toe (XO) Game

## Project Overview
This is a **Tic-Tac-Toe (XO)** web application built with **ASP.NET Core MVC**. It allows players to compete against each other or against an AI opponent. The game has a responsive UI, real-time updates, and uses **SQL Server** to store game states.

## Features
- **Single-player mode**: Play against a computer (AI) opponent.
- **Multiplayer mode**: Two human players can compete.
- **AI opponent**: The AI makes strategic moves based on predefined logic.
- **Responsive UI**: Works seamlessly on desktops, tablets, and mobile devices.
- **Game state management**: Stores and retrieves data from a SQL Server database.
- **Win detection**: Automatically identifies winners or a draw.
- **Easy reset**: Players can restart the game at any time.

## How to Play
1. **Open the homepage** and choose a game mode:
   - **Against AI (Computer)**
   - **Against another player**
2. **Take turns** placing 'X' or 'O' in an empty cell.
3. **Winning or Draw**:
   - The game declares a winner if any row, column, or diagonal has the same symbol.
   - If all cells are filled with no winner, it’s a draw.
4. **Restart** the game from the end screen or the menu if you want to play again.

## AI Opponent (Computer Mode)
- The AI automatically places 'O' when playing against a human (X).
- Basic logic can be:
  - **Random** (chooses any available spot).
  - **Rule-Based** (blocks your winning moves, tries to create a winning line).
  - **MiniMax** (optional advanced strategy for optimal moves).

## Project Structure


📂 Project Root
├── 📂 Controllers
│   ├── HomeController.cs
│   ├── GameController.cs
│   ├── ComputerGameController.cs
│
├── 📂 Models
│   ├── Game.cs
│   ├── MoveRequest.cs
│   ├── GameDbContext.cs
│
├── 📂 Views
│   ├── 📂 Home
│   │   ├── Index.cshtml
│   │   ├── Privacy.cshtml
│   ├── 📂 Game
│   │   ├── Index.cshtml
│   │   ├── _GameBoard.cshtml
│   │   ├── Winner.cshtml
│   ├── 📂 ComputerGame
│   │   ├── Index.cshtml
│   │   ├── Winner.cshtml
│   ├── 📂 Shared
│       ├── _Layout.cshtml
│       ├── _ValidationScriptsPartial.cshtml
│       ├── _ViewImports.cshtml
│       ├── _ViewStart.cshtml
│
├── appsettings.json
├── Program.cs


## Setup Instructions


## Clone the repository:

git clone https://github.com/your-repo/tic-tac-toe.git
cd tic-tac-toe

## Setup the database:

Ensure SQL Server is running.

Update appsettings.json with your database connection string.

## Run database migrations:

dotnet ef database update

## Run the application:

dotnet run

## Open in browser:

Navigate to http://localhost:5000/ to start playing.
