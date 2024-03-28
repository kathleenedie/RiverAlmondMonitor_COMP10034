const {nextui} = require("@nextui-org/react");

/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./src/**/*.{js, jsx, html}",
    "./node_modules/@nextui-org/theme/dist/**/*.{js,ts,jsx,tsx}",
  ],
  theme: {
    extend: {
      colors:{
        japonica: '#DC7C6C',
        polyGreen: '#314D35',
        catalina: '#084075',
        viking: '#6CABDC',
        columbia: '#CAD9E9',
        athensGray: '#E9E8ED'

      }
    },
    screens: {
      'sm': '640px',
      'md': '768px',
      'lg': '1024px',
      'xl': '1280px',
      '2xl': '1536px',
    }
  ,

  },
  plugins: [
    require('@tailwindcss/forms'),
    nextui()
  ],
}

