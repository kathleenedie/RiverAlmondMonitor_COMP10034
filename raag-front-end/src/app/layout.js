import NavBar from './Components/NavBar'
import "./globals.css"
import { Inter } from 'next/font/google'

const inter = Inter({ subsets: ['latin'] })

export const metadata = {
  title: 'A River Almond Information Site',
  description: 'A photo mapping tool',
}

export default function RootLayout({ children }) {
  return (
    <html lang="en">
      
      <body className="bg-athensGray">
        
          <NavBar />
          {children}
        
      </body>
      
    </html>
  )
}
