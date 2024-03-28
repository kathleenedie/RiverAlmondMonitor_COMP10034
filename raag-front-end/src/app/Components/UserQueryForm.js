"use client"
import { useRef, useEffect, useState } from "react";
import 'chartjs-adapter-date-fns';
import UserQueryChart from "./UserQueryChart";
import Downloader from "./Downloader";

export default function UserQueryForm(){

    const [formData, setFormData] = useState({
        parameter: "",
        startDate: "",
        endDate: "",
      });
    const [queryResultData, setQueryResultData] = useState([]);
    const [loadingState, setLoadingState] = useState('in_progress');

    const handleInput = (e) => {
        const fieldName = e.target.id;
        const fieldValue = e.target.value;
      
        setFormData(() => ({
          ...formData,
          [fieldName]: fieldValue
        }));
      }

    async function handleFormSubmit (e){
        e.preventDefault()

        try{
        const submitData = new FormData();
        submitData.append("parameter", formData.parameter)
        submitData.append('startDate', formData.startDate)
        submitData.append('endDate', formData.endDate)
      
        console.log("this is the data to submit" + submitData);
      
        const res = await fetch('https://localhost:7064/api/Dashboard/UserQuery',{
            method: 'POST',
            body: submitData,
            headers: {// Authorization: needed
            }
        })
     
        if(!res.ok){
            throw new Error("error status: " + res.status);
        }
        
        const data = await res.json();
            console.log("query response data: ", data)
            setQueryResultData(data)
                
            setFormData({
                parameter: "",
                startDate: "",
                endDate: "",
            });
        
            setLoadingState('complete');
        }catch(error) {
                console.error(error)
            }
          }

    return(
        <div className="aspect-auto flex justify-center">
          <div className="h-[40vh] flex justify-center">
          <div className="mt-6 mb-4 max-w-md">
            <div className="grid grid-cols-1 gap-6"> 
            <div className="block">
            <form name="user-form" className="form-input bg-columbia shadow-2xl border-catalina rounded-lg p-8"  onSubmit={handleFormSubmit}> 
              <div className="block pt-2">
                <label htmlFor="parameter">Data Type</label>
                <select className="form-control mt-1 block w-full" id="parameter" required value={formData.parameter ? formData.parameter : ''} onChange={handleInput}>
                    <option value="" >Please choose an option</option>
                    <option>rain</option>
                    <option>flow</option>
                    <option>level</option>
                </select>
              </div>      
              <div className="block pt-2">
                <label htmlFor="startDate">Start Date</label>
                <input type="date" className="mt-1 block w-full" id="startDate" required onChange={handleInput} value={formData.startDate}/>
              </div>
              <div className="block pt-2">
                <label htmlFor="endDate">End Date</label>
                <input type="date" className="mt-1 block w-full" id="endDate" required onChange={handleInput} value={formData.endDate}/>
              </div>
              <div className="flex justify-center p-3" >
              <div className="block pt-2">
              <button type="submit" className="rounded-lg bg-japonica text-white p-2 hover:shadow-lg hover:bg-polyGreen text-2xl">Submit</button>
              </div>
              </div>
              
            </form>
            </div>
            </div>
            </div>
            </div>

            <div className="h-[50vh] flex justify-center">
            <div className="mt-4 mb-4 max-w-md">
            <div className="grid grid-cols-1 gap-6 ml-6"> 
            <div className="block items-center">
            {loadingState === 'complete' && <UserQueryChart className="p-4" data={queryResultData} />}
        
            {loadingState === 'complete' && <Downloader data={queryResultData}/>}
            </div>
            </div>
            </div>
            </div>
            
            
        </div>
        
    )


           
}