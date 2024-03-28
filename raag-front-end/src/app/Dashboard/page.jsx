"use client"
import LevelChart from "../Components/LevelChart"
import RainChart from "../Components/RainChart"
import FlowChart from "../Components/FlowChart"
import UserQueryForm from "../Components/UserQueryForm"
import Condition from "../Components/ConditionTable"
import { NextUIProvider } from "@nextui-org/react"
import React from "react";

export default function Dashboard(){
    return(

        // Ideally reusable data fetch and chart render using passed in parameters!

        <NextUIProvider>
            <div className="bg-athensGray">
                <div className="h-[20vh] flex justify-center">
                    <div className="h-[20vh] rounded-xl p-6 mt-6 bg-viking flex justify-center">
                        <h1 className="p-8">A Dashbboard to show annual and live data on the River Almond.</h1>
                    </div>
                </div>

                <div className="justify-center bg-athensGray mt-6 gap-6">
                    <Condition />
                </div>

                <div className="flex flex-row justify-center bg-athensGray mt-6 gap-4">
                    <FlowChart/>
                    <LevelChart/>
                    <RainChart/>
                </div>
                <div className="flex flex-row justify-center bg-athensGray mt-6">
                    <UserQueryForm />
                </div>
            </div>
        </NextUIProvider>
    )
}