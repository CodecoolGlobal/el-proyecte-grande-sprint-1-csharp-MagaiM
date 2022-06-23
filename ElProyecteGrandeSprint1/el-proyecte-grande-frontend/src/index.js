import ReactDOM from 'react-dom/client';
import './Design/index.css';
import './Design/App.css';
import './Design/Imgs/KVMResized.jpg';
import './Design/Imgs/KVM.jpg';
import './Design/Imgs/BaseProfilepicture.jpg';
import 'bootstrap/dist/css/bootstrap.min.css';
import App from './App';
import reportWebVitals from './reportWebVitals';
import {BrowserRouter, Routes, Route} from "react-router-dom";
import Home from './routes/Home';
import News from './routes/News';
import Deals from './routes/Deals';
import RecentNews from './routes/RecentNews';
import OtherNews from './routes/OtherNews';
import Login from "./routes/Login";
import Register from "./routes/Register";
import React from 'react';
import { CookiesProvider } from 'react-cookie';

const root = ReactDOM.createRoot(document.getElementById('root'));

root.render(
    <React.StrictMode>
        <CookiesProvider>
            <BrowserRouter>
                <Routes>
                    <Route path="/" element={<App/>}>
                        <Route path="/" element={<Home/>}/>
                        <Route path="/news" element={<News/>}>
                            <Route path='' element={<RecentNews/>}/>
                            <Route path='/news/other-news' element={<OtherNews/>}/>
                        </Route>
                        <Route path="deals" element={<Deals/>}/>
                        <Route path="/login" element={<Login postData={postData} getData={getData} />}/>
                        <Route path="/register" element={<Register postData={postData}/>}/>
                        <Route path="*" element={
                            <main style={{padding: "1rem"}}>
                                <p>There's nothing here!</p>
                            </main>}/>
                    </Route>
                </Routes>
            </BrowserRouter>
        </CookiesProvider>
    </React.StrictMode>
);

async function getData(url) {

    let response = await fetch(`https://localhost:44321/${url}`);
    return response.json();
}

async function postData(url, formData) {

    let response = await fetch(`https://localhost:44321/${url}`, {
            method: "POST",
            headers: {
                'content-type': 'application/json',
            },
            body: JSON.stringify(formData),
        })
        return response.json();
}
// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();