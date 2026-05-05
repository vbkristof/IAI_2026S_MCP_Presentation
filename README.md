# MCP Inventory Management Demo

A demonstration of the Model Context Protocol (MCP) integration with an inventory management system. This project showcases how AI assistants can interact with enterprise data through standardized MCP tools.

## 📋 Overview

This project consists of two main components:

- **McpServer**: A .NET 8 Web API that implements an MCP server, exposing inventory management tools
- **McpClient**: A client configuration for connecting to the MCP server using `mcphost`

## 🛠️ Tech Stack

- **.NET 8** - Web API framework
- **Entity Framework Core** - ORM with PostgreSQL
- **ModelContextProtocol.AspNetCore** - MCP server implementation
- **PostgreSQL** - Database
- **mcphost** - MCP client with Ollama (qwen3 model)

## 🚀 Features

The MCP server exposes three inventory management tools:

- **list_inventory_items** - Retrieve all inventory items from the ERP system
- **get_low_stock_items** - Find items where current stock is below minimum threshold
- **create_reorder_suggestion** - Generate reorder suggestions based on target stock levels

## 📦 Project Structure

```
.
├── McpServer/              # .NET 8 MCP Server
│   ├── Data/              # Database context and migrations
│   ├── Models/            # Entity models (InventoryItem)
│   ├── Repositories/      # Data access layer
│   ├── Services/          # Business logic
│   └── Tools/             # MCP tool definitions
└── McpClient/             # MCP client configuration
    ├── mcp-servers.json   # Server connection config
    └── start-mcphost.sh   # Startup script
```

## 🏃 Getting Started

### Prerequisites

- .NET 8 SDK
- PostgreSQL
- mcphost CLI
- Ollama with qwen3 model

### Server Setup

1. Navigate to the McpServer directory:
   ```bash
   cd McpServer
   ```

2. Configure the database connection in `appsettings.json`

3. Run database migrations:
   ```bash
   dotnet ef database update
   ```

4. Start the server:
   ```bash
   dotnet run
   ```

The server will be available at `http://localhost:5007`

### Client Setup

1. Navigate to the McpClient directory:
   ```bash
   cd McpClient
   ```

2. Start the MCP host:
   ```bash
   ./start-mcphost.sh
   ```

## 📄 License

This project is licensed under the terms of the LICENSE file.
