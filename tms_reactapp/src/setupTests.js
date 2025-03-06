// jest-dom adds custom jest matchers for asserting on DOM nodes.
// allows you to do things like:
// expect(element).toHaveTextContent(/react/i)
// learn more: https://github.com/testing-library/jest-dom
import '@testing-library/jest-dom/extend-expect';

// Polyfills for TextEncoder and TextDecoder (in case you're using them in your tests)
global.TextEncoder = require("util").TextEncoder;
global.TextDecoder = require("util").TextDecoder;

