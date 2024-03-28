import React, { useEffect, useState } from 'react';

export default function Downloader({data}){
  const downloadData = data;

  const downloadCSV = () => {
    const csvContent = "data:text/csv;charset=utf-8," +
      "id, timestamp, value, timeSeriesId, description, stationName\n" +
      downloadData.map(datum =>
        `${datum.id}, ${datum.timestamp},${datum.value},${datum.timeSeriesId},${datum.description},${datum.stationName}`
      ).join("\n");

    const encodedUri = encodeURI(csvContent);
    const link = document.createElement("a");
    link.setAttribute("href", encodedUri);
    link.setAttribute("download", "almond_data.csv");
    document.body.appendChild(link);
    link.click();
  };

  return (
    <div className="flex justify-center">
      <button className="rounded-lg bg-japonica text-white p-2 hover:shadow-lg hover:bg-polyGreen text-2xl" onClick={downloadCSV}>Download CSV</button>
    </div>
  );
};