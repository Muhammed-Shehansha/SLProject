// jest.config.js
module.exports = {
    transform: {
      "^.+\\.[t|j]sx?$": "babel-jest",
    },
    transformIgnorePatterns: [
      "/node_modules/(?!axios|other-esm-libraries).+\\.js$"  // Add axios or any other ESM library here
    ],
    setupFilesAfterEnv: ["/src/setupTests.js"],
  };
  