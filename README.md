<!DOCTYPE html>
<html lang="en">
<head>
  <meta charset="UTF-8">
  <title>🍦 Ice Cream Shop Website README</title>
  <style>
    body { font-family: Arial, sans-serif; margin: 20px; line-height: 1.6; }
    h1, h2, h3 { color: #333; }
    pre { background: #f5f5f5; padding: 10px; border-radius: 5px; }
    code { color: #c7254e; background: #f9f2f4; padding: 2px 4px; border-radius: 4px; }
  </style>
</head>
<body>
  <h1>🍦 Ice Cream Shop Website</h1>
  <p>
    This is a full‑featured web application where users can browse the menu, add items to the cart, place orders,
    and find store locations. Admins can also manage menu items and update order statuses.
  </p>

  <h2>🚀 Features</h2>
  <h3>User Features</h3>
  <ul>
    <li><strong>Browse Menu:</strong> View available ice cream flavors and products.</li>
    <li><strong>Add to Cart:</strong> Select items and add them to a shopping cart.</li>
    <li><strong>Place Order:</strong> Complete checkout and submit orders.</li>
    <li><strong>Store Locator:</strong> View locations of all stores on a map.</li>
    <li><strong>Order Tracking:</strong> Check the status of current and past orders.</li>
  </ul>

  <h3>Admin Features</h3>
  <ul>
    <li><strong>Enable/Disable Menu Items:</strong> Toggle availability of any item.</li>
    <li><strong>Order Management:</strong> Update order statuses (e.g., pending, preparing, ready, completed).</li>
  </ul>

  <h2>🛠️ Technologies Used</h2>
  <ul>
    <li>Frontend: HTML, CSS, JavaScript</li>
    <li>Backend: Node.js, Express.js</li>
    <li>Database: MongoDB</li>
    <li>Auth: JWT (JSON Web Tokens)</li>
    <li>Maps Integration: Google Maps API</li>
  </ul>

  <h2>📦 Installation</h2>
  <ol>
    <li><strong>Clone the repo:</strong>
      <pre><code>git clone https://github.com/yourusername/ice‑cream‑shop‑website.git</code></pre>
    </li>
    <li><strong>Go into the folder:</strong>
      <pre><code>cd ice‑cream‑shop‑website</code></pre>
    </li>
    <li><strong>Install dependencies:</strong>
      <pre><code>npm install</code></pre>
    </li>
    <li><strong>Setup .env:</strong>
      <pre><code>
PORT=3000
MONGODB_URI=your_mongodb_connection_string
JWT_SECRET=your_jwt_secret
GOOGLE_MAPS_API_KEY=your_google_maps_api_key
      </code></pre>
    </li>
    <li><strong>Start the server:</strong>
      <pre><code>npm start</code></pre>
    </li>
  </ol>

  <h2>📁 Project Structure</h2>
  <pre><code>
ice‑cream‑shop‑website/
├── controllers/
├── models/
├── routes/
├── public/
├── views/
├── .env
├── app.js
└── package.json
  </code></pre>

  <h2>📌 Usage</h2>
  <h3>User</h3>
  <ul>
    <li>Browse to <code>http://localhost:3000/</code> to view the menu, add items to cart, checkout, and locate stores.</li>
    <li>Track order status and review past orders.</li>
  </ul>
  <h3>Admin</h3>
  <ul>
    <li>Browse to <code>http://localhost:3000/admin</code> for admin dashboard.</li>
    <li>Log in, then manage availability of menu items and update order statuses.</li>
  </ul>

  <h2>🤝 Contributing</h2>
  <p>
    Contributions are welcome! Please fork the project, make enhancements or fixes, and open a pull request.
  </p>

  <h2>📄 License</h2>
  <p>This project is licensed under the <a href="LICENSE">MIT License</a>.</p>
</body>
</html>
