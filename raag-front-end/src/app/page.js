"use client"
import React, {useState} from "react";
import {useRouter} from "next/navigation";

export default function Home() {

const [selectedImage, setSelectedImage] = useState({
  imageSrc: "",
  imageFile: null

});
const [formData, setFormData] = useState({
  report: "",
  firstName: "",
  lastName: "",
  email: "",
  isImagePermission: false
});

const [location, setLocation] = useState({
  latitude: null,
  longitude: null
})
const router = useRouter();

function handleChange (target){
  if (target.files.length !==0) {
      const file = target.files[0];
      
      const newUrl = URL.createObjectURL(file);
      setSelectedImage({
        imageSrc: newUrl,
        imageFile: file});
      console.log("selectedImageURL: " + selectedImage.URL)
    }
}


const handleInput = (e) => {
  const fieldName = e.target.id;
  const fieldValue = e.target.value;

  setFormData(() => ({
    ...formData,
    [fieldName]: fieldValue
  }));

  console.log("formData: "+ formData.lastName)
}

const handleCheckboxInput = () => {
  setFormData(() => ({
    ...formData,
    isImagePermission: !formData.isImagePermission
  }))
}

function handleLocationClick() {
  if (navigator.geolocation) {
    navigator.geolocation.getCurrentPosition(success, error);
  } else {
    console.log("Geolocation not supported");
  }
}

function success(position) {
  const latitude = position.coords.latitude;
  const longitude = position.coords.longitude;
  setLocation(() => ({
    ...location,
    latitude: latitude,
    longitude: longitude
  }));
  console.log(`Latitude: ${latitude}, Longitude: ${longitude}`);
}

function error() {
  console.log("Unable to retrieve your location");
}

const validate = () =>{
  let temp = {}
  temp.firstName = formData.firstName==""?false:true;
  temp.imageSrc = formData.imageSrc==null?false:true;
  //setErrors(temp)
  return Object.values(temp).every(x => x==true)
}

const handleFormSubmit = e => {
  e.preventDefault()
  const submitData = new FormData();
  submitData.append('latitude', location.latitude)
  submitData.append('longitude', location.longitude)
  submitData.append('report', formData.report)
  submitData.append('firstName', formData.firstName)
  submitData.append('lastName', formData.lastName)
  submitData.append('email', formData.email)
  submitData.append('isImagePermission', formData.isImagePermission)
  submitData.append('imageFile', selectedImage.imageFile)

  console.log("this is the data to submit" + submitData);

  const res = fetch('https://localhost:7064/api/UserReport/UserReport',{
      method: 'POST',
      body: submitData,
      headers: {// Authorization: needed
      }
  })
  .then((res) => {

    console.log("response: ", res)
      if(!res.status === 201){
        throw new Error("error status: " + res.status);
      }
      setFormData({
        image: "",
        report: "",
        firstName: "",
        lastName: "",
        email: "",
        isImagePermission: ""
      });
      router.push('/Report')
    })
  }

return (
  <main>
    <div className=" flex justify-center mt-10">
            <div className="bg-viking rounded-xl w-[70vw] text-center p-5">
              <h1 className="font-bold text-2xl text-catalina">Welcome to River Almond Action Group's River Monitor.</h1>
              <p> We are collected as many reports from the local community on the state of the Almond to support our ongoing campaign for a clean Almond and its tributries.</p>
              <p>If you see or smell any debris, sludge, discoloured water, foam or rubbish in the Almond why not take a photo and post it to our <a rel="noopener" href="/Almond" className="text-catalina font-extrabold">Map</a>?</p>
              <p>Simply get your location, enter some details and take a photo on your phone,</p><p className="text-white"> using the form below.</p>
              <p className="text-sm">Credit will be given if you choose to let RAAG use your photo in our work.</p>
            </div>
        </div> 
    <div className="aspect-auto flex justify-center bg-athensGray">
      <div className="flex content-center">
        <div className="mt-8 max-w-md flex justify-center">
        <div className="grid grid-cols-1 gap-6 contents-center"> 
            <div className="flex justify-center p-2"> 
            <div className="block">
              {!location.latitude && !location.longitude ? <button className="rounded-lg bg-polyGreen text-white p-2 font-bold hover:shadow-lg" onClick={handleLocationClick}>Get Location</button> : null}
            </div>
            </div>
            <form className="form-input bg-columbia rounded-lg p-10 shadow-2xl border-catalina"  onSubmit={handleFormSubmit}>
              
              <div className="block pt-2">
                <label htmlFor="report" className="font-bold">Your report of the state of the Almond</label>
                <textarea type="textarea" className="form-textarea mt-1 block w-full" id="report" rows="5"  onChange={handleInput} value={formData.report}/>
              </div>
              <div className="block pt-2">
                <label htmlFor="firstName" className="font-bold">First Name</label>
                <input type="text" className="mt-1 block w-full" id="firstName" onChange={handleInput} value={formData.firstName}/>
              </div>
              <div className="block pt-2">
                <label htmlFor="lastName" className="font-bold">Last Name</label>
                <input type="text" className="mt-1 block w-full" id="lastName" onChange={handleInput} value={formData.lastName}/>
              </div>
              <div className="block pt-2">
                <label htmlFor="email" className="font-bold">Email (optional)</label>
                <input type="email" className="mt-1 block w-full" id="email" onChange={handleInput} value={formData.email}/>
              </div>
              <div className="flex justify-center mt-2 mb-2">
              <div className="block pt-2">
              <input 
                  type="file"
                  accept="image/*" 
                  capture="environment"
                  id="file-input"
                  style={{display:'none'}}
                  onChange={(e) => handleChange(e.target)}/>
              <label htmlFor="file-input" className="rounded-lg bg-polyGreen text-athensGray font-bold p-2 hover:shadow-lg">Take photo</label>
              </div>
              </div>
              <div className='flex justify-center'>
              <div className=" block pt-2">
                  <ul >
                    <img className=" text-athensGray" src={selectedImage.imageSrc} alt="your photo" style={{width:300}} />
                  </ul>
              </div>
              </div>

              <div className="block pt-2 justify-center">
                <label htmlFor="isImagePermission" className="pr-2 font-bold">Can RAAG use this photo in our work?</label>
                <input type="checkbox" className="form-checkbox mt-1 block" id="isImagePermission" onChange={handleCheckboxInput} value={formData.isImagePermission}/>
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

  </main>
)
}
