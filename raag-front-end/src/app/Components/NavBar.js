import React from 'react'
import Link from 'next/link'
import Image from 'next/image'
import {useRouter} from 'next/navigation'
import logo from '../../../public/raag-logo.png'

export default function Navbar() {
    const linkStyle = {
        textDecoration: "none"
    };
    
    //  items-center

    return (
        <nav className="bg-columbia w-full flex justify-between h-35 pl-10 pr-10 .box">
            <Image src={logo} height="70" alt="logo" className="p-2"/>
                    <div className="text-catalina w-full font-semibold text-3xl flex items-center ml-10">
                            <h1 className="">River Almond Monitor</h1>                        
                    </div>
                    <div className="flex justify-right items-center">
                        <ul className="flex gap-6 list-none text-blue-900">
                            <li><Link href="/" className="hover:bg-slate-600 hover:text-white hover:p-3 hover:rounded-md">Report</Link></li>
                            <li><Link href="/Dashboard" className="hover:bg-slate-600 hover:text-white hover:p-3 hover:rounded-md">Dashboard</Link></li>
                            <li><Link href="/Almond" className="hover:bg-slate-600 hover:text-white hover:p-3 hover:rounded-md">Map</Link></li> 
                            <li><Link href="/exit" className="hover:bg-slate-600 hover:text-white hover:p-3 hover:rounded-md">Exit</Link></li>  
                        </ul>
                    </div>
    </nav>
    )
}
