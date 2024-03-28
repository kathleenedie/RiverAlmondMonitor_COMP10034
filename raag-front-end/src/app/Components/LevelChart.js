"use client"
import { useRef, useEffect, useState } from "react";
import {Chart} from "chart.js/auto";
import 'chartjs-adapter-date-fns';

export default function LevelChart(){

     //Need to handle loading state

    const chartRef = useRef(null);
    const [levelData, setLevelData] = useState([]);
    const [reportData, setReportData] = useState([]);

    useEffect(() => {
        const fetchData = async () => {
            const res = await fetch("https://localhost:7064/api/Dashboard/LevelData")
            if(!res.ok){
                console.error("Bad response")
            }

            const data = await res.json();
            console.log("level data: ", data)
            setLevelData(data)
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

            var craigiehallData = new Array();
            levelData.forEach((datum) => {
                if(datum.timeSeriesId === 54250010){        
                    craigiehallData.push({x: datum.timestamp, y: datum.value})
                }
                });


            var almondellData = new Array();
            levelData.forEach((datum) => {
                if(datum.timeSeriesId === 54283010){        
                    almondellData.push({x: datum.timestamp, y: datum.value})
                }
                });

            var whitburnData = new Array();
            levelData.forEach((datum) => {
                if(datum.timeSeriesId === 54405010){        
                    whitburnData.push({x: datum.timestamp, y: datum.value})
                }
                });

            const reportPlotData = reportData.map((items) => {
                return {x: items.timestamp, y: 1}})


            const newChart = new Chart(context, {
                data: {
                    datasets: [
                        {
                            type: 'line',
                            label: "Craigiehall Level",
                            data: craigiehallData,
                            backgroundColor: "gray",
                            borderWidth: 2,
                            borderColor: "gray"
                        },
                        {
                            type: 'line',
                            label: "Almondell Level",
                            data: almondellData,
                            backgroundColor: "blue",
                            borderWidth: 2,
                            borderColor: "blue"
                        },
                        {
                            type: 'line',
                            label: "Whitburn Level",
                            data: whitburnData,
                            backgroundColor: '#DC7C6C',
                            borderWidth: 2,
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
                            beginAtZero: false
                        }
                    },
                    plugins:{
                        title:{
                            display: true,
                            text:"Water level at Monitoring Stations on the Almond",
                            position: 'bottom',
                            color: '#084075'
                        }
                    }
                }
            });

            chartRef.current.chart = newChart;

        }
    }, [levelData]);

    
    return (
    <div className="bg-white rounded-lg shadow-2xl pt-8 pb-8 flex justify-center" style={{ position: "relative", width: "40vw", height: "40vh"}}>
        <canvas ref={chartRef}/>
    </div>
    )
}