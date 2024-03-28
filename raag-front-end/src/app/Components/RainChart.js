"use client"
import { useRef, useEffect, useState } from "react";
import {Chart} from "chart.js/auto";
import 'chartjs-adapter-date-fns';

export default function RainChart(){

    const chartRef = useRef(null);
    const [rainData, setRainData] = useState([]);
    const [reportData, setReportData] = useState([]);

     //Need to handle loading state

    useEffect(() => {
        const fetchData = async () => {
            const res = await fetch("https://localhost:7064/api/Dashboard/RainData")
            if(!res.ok){
                console.error("Bad response")
            }

            const data = await res.json();
            console.log("rain data: ", data)
            setRainData(data)
        }

        fetchData();
    }, [])

    useEffect(() => {
        const fetchData = async () => {
            const res = await fetch('https://localhost:7064/api/UserReport/UserReports')
            if(!res.ok){
                console.error("Bad response")
            }

            const data = await res.json();
            console.log("rain data: ", data)
            setReportData(data)
        }

        fetchData();
    }, [])

    useEffect(()=> {
        if(chartRef.current){
            if(chartRef.current.chart){
                chartRef.current.chart.destroy()
            }

            const context = chartRef.current.getContext("2d");

            const rainPlotData = rainData.map((items) => {
                return {x: items.timestamp, y: items.value}});

            const reportPlotData = reportData.map((items) => {
                return {x: items.timestamp, y: 4}})


            const newChart = new Chart(context, {
                data: {
                    datasets: [
                        {
                            type: 'bar',
                            label: "Whitburn",
                            data: rainPlotData,
                            backgroundColor: '#DC7C6C',
                            borderWidth: 4,
                            borderColor: '#DC7C6C'
                        },
                        {
                            type: 'scatter',
                            label: "User Report",
                            data: reportPlotData,
                            backgroundColor: ["green"],
                            borderWidth: 4,
                            borderColor: "green"
                        },

                    ]
                },
                options : {
                    // responsive: true,
                    scales: {
                        x: {
                            type: "time",
                            time: {
                                unit: "day"
                            }
                        },
                        y: {
                            beginAtZero: false,
                            ticks: {
                                display: true
                            }
                        }
                    },
                    plugins:{
                        title:{
                            display: true,
                            text:"Rainfall(mm) at Monitoring Stations on the Almond",
                            position: 'bottom',
                            color: '#084075'
                        }
                    }
                }
            });

            chartRef.current.chart = newChart;

        }
    }, [rainData]);

    
    return (
    <div className="bg-white rounded-lg shadow-2xl pt-8 pb-8 flex justify-center" style={{ position: "relative", width: "40vw", height: "40vh"}}>
        <canvas ref={chartRef}/>

    </div>
    )
}