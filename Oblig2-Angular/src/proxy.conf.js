const PROXY_CONFIG = [
  {
    context: [
      "/api",
    ],
    target: "https://localhost:44335",
    secure: false
  }
]

module.exports = PROXY_CONFIG;
