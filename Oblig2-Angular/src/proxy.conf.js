const PROXY_CONFIG = [
  {
    context: [
      "/oblig",
      "/Oblig"
    ],
    target: "https://localhost:44335",
    secure: false
  }
]

module.exports = PROXY_CONFIG;
