"use client";

import ReactMapGl, { NavigationControl, GeolocateControl, Marker, Popup} from "react-map-gl";
import "mapbox-gl/dist/mapbox-gl.css";
import classes from '../../input.css';
import { MdCompassCalibration, MdOutlineAssignmentTurnedIn } from "react-icons/md";
import { MdWaterDamage } from "react-icons/md";
import {useState, useRef, useEffect} from "react";
import { NextResponse } from "next/server";

export default function Almond() {
	const mapboxToken = process.env.NEXT_PUBLIC_MAPBOX_ACCESS_TOKEN;
    const [stations, setStations] = useState([]);
    const [selectedLocation, setSelectedLocation] = useState({});
    const [measurements, setMeasurements ] = useState([]);
    const [selectedMeasurement, setSelectedMeasurement] = useState({});
    const [userReports, setUserReports] = useState([]);
    const [selectedReport, setSelectedReport] = useState([]);
    const measurementEndpoint = 'https://localhost:7064/api/MapData/MapMeasurements';
    const userReportEndpoint = 'https://localhost:7064/api/MapData/UserReports';
    const stationEndpoint = 'https://localhost:7064/api/MapData/Stations';

    useEffect( () => {fetch(stationEndpoint)
        .then((response) => response.json())
        .then((data) => setStations(data))
    }, [] )

    useEffect( () => {fetch(measurementEndpoint)
        .then((response) => response.json())
        .then((data) => setMeasurements(data))
    }, [] )

    useEffect( () => {fetch(userReportEndpoint)
        .then((response) => response.json())
        .then((data) => setUserReports(data))
    }, [] )
    
   
	return (
		<main className="">
            <div className="mt-8 flex justify-center">

			<ReactMapGl
                style={{ width: '70%', height: '700px', backgroundcolor: '#E9E8ED' }}
				mapStyle="mapbox://styles/mapbox/streets-v12"
                mapboxAccessToken={mapboxToken}
				initialViewState={{ latitude: 55.89512862734309, longitude: -3.4858196169457867, zoom: 10 }}
				maxZoom={20}
				minZoom={3}
			>
                <GeolocateControl position="top-left" />
                <NavigationControl position="top-left" />

                {stations.map((station, index) => (
                    <div >
                        <Marker
                        key={index}
                        latitude={station.latitude}
                        longitude={station.longitude}
                        offsetLeft={-20}
                        offsetTop={-10}>
                            <a onClick={() => {
                                setSelectedLocation(station);
                            }}>
                                <MdWaterDamage size={40} color="blue" />
                            </a>
                        </Marker> 
                    </div>
                    ))},
                {measurements.map((measurement) => (
                    <div >
                        <Marker
                        key={measurement.stationId}
                        latitude={measurement.latitude}
                        longitude={measurement.longitude}
                        offsetLeft={-600}
                        offsetTop={-400}>
                            <a onClick={() => {
                                setSelectedMeasurement(measurement);
                            }}>
                                <MdCompassCalibration size={30} color="lightblue" />
                            </a>
                        </Marker>

                        {selectedMeasurement.stationId === measurement.stationId ? (
                        <Popup
                        onClose={() => setSelectedMeasurement({})}
                        closeOnClick={false}
                        latitude={measurement.latitude}
                        longitude={measurement.longitude}
                        offsetLeft={-400}
                        offsetTop={1600}
                        style={{backgroundColor:'lightgray', color:'black', width:'150px', fontStyle: 'italic'}}
                        color="blue">

                            <div >
                                <h2 style={{color:'blue', fontWeight:'bold'}}>Latest Measurement</h2> 
                                {selectedMeasurement.values.map((value, index) => ( 
                                    <div key={index}>
                                        <div>{value.stationParameterName}: {value.value}</div>
                                    </div>
                                ))}
                            </div>
                            
                        </Popup>) : []}
                    </div>
                ))}

                {userReports.map((report, index) => (
                    <div >
                        <Marker
                        key={index}
                        latitude={report.latitude}
                        longitude={report.longitude}
                        offsetLeft={-600}
                        offsetTop={-400}>
                            <a onClick={() => {
                                setSelectedReport(report);
                            }}>
                                <MdOutlineAssignmentTurnedIn size={30} color="green" />
                            </a>
                        </Marker>

                        {selectedReport.id === report.id ? (
                            <Popup
                            onClose={() => setSelectedReport({})}
                            closeOnClick={false}
                            latitude={report.latitude}
                            longitude={report.longitude}
                            offsetLeft={-400}
                            offsetTop={1600}
                            color="green">
                            
                                <div className="p-3" key={report.id}>      
                                    <h2>{selectedReport.firstName}'s report</h2>
                                    <p>{selectedReport.report}</p>
                                    <p>{selectedReport.timestamp}</p>
                                    <img src={selectedReport.imageSrc} alt="report image" style={{width:150, border:5, borderColor:"black"}}></img>
                                </div>
                        
                            </Popup>) : []}
                    </div>
                ))}
            </ReactMapGl>
            </div>
		</main>
	);
}
