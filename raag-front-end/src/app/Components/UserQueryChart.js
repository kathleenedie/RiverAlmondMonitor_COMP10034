"use client"
import { useRef, useEffect, useState } from "react";
import {Chart} from "chart.js/auto";
import 'chartjs-adapter-date-fns';

export default function UserQueryChart({data}){

    const chartRef = useRef(null);
    var queryResultData = data;

     //Need to handle loading state

          useEffect(()=> {
            if(chartRef.current){
                if(chartRef.current.chart){
                    chartRef.current.chart.destroy()
                }
    
                const context = chartRef.current.getContext("2d");
    
                var dataCraigiehall = new Array();
                var dataAlmondell = new Array();
                var dataWhitburn = new Array();
                queryResultData.forEach((datum) => {
                    if(datum.stationName === "Craigiehall"){        
                        dataCraigiehall.push({x: datum.timestamp, y: datum.value})
                    }
                    if(datum.stationName === "Almondell"){        
                        dataAlmondell.push({x: datum.timestamp, y: datum.value})
                    }
                    if(datum.stationName === "Whitburn"){        
                        dataWhitburn.push({x: datum.timestamp, y: datum.value})
                    }
                    });
    
    
    
                const newChart = new Chart(context, {
                    data: {
                        datasets: [
                            {
                                type: 'line',
                                label: "Craigiehall",
                                data: dataCraigiehall,
                                backgroundColor: "gray",
                                borderWidth: 2,
                                borderColor: "gray"
                            },
                            {
                                type: 'line',
                                label: "Almondell",
                                data: dataAlmondell,
                                backgroundColor: "blue",
                                borderWidth: 2,
                                borderColor: "blue"
                            },
                            {
                                type: 'line',
                                label: "Whitburn",
                                data: dataWhitburn,
                                backgroundColor: '#DC7C6C',
                                borderWidth: 2,
                                borderColor: '#DC7C6C'
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
                                text:"Monitoring Stations on the Almond",
                                position: 'bottom',
                                color: '#084075'
                            }
                        }
                    }
                });
    
                chartRef.current.chart = newChart;
    
            }
        }, [data]);

    return(
        <div>

            <div className="bg-white rounded-lg shadow-2xl p-2 flex justify-center mb-6" style={{ position: "relative", width: "40vw", height: "40vh"}}>
                <canvas ref={chartRef}/>
            </div>

        </div>
    )


           
}


