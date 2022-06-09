import React, { Component, useEffect } from 'react';
import PropTypes from 'prop-types'
import { useState } from "react";
import { DropdownButton, Dropdown} from 'react-bootstrap';
import 'bootstrap/dist/css/bootstrap.min.css';

const Deals = ({fetchData}) => {
    const [dealsData, setDealsData] = useState([]);

    useEffect(() => {
        fetchData('https://localhost:7064/Deals?sortBy=Deal Rating')
        .then(dealsData => {
            setDealsData(dealsData);
        })
    }, [])
    const dealsPageContent = 
    (<div className="deals-page-container">
        <div className='collum'>
        <DropdownButton id="dropdown-basic-button" title="Order by" variant='secondary' menuVariant='dark'>
          <Dropdown.Item onClick={()=>fetchData('https://localhost:7064/Deals?sortBy=Deal Rating').then(dealsData=>{setDealsData(dealsData)})}>Deal Rating</Dropdown.Item>
          <Dropdown.Item onClick={()=>fetchData('https://localhost:7064/Deals?sortBy=Title').then(dealsData=>{setDealsData(dealsData)})}>Title</Dropdown.Item>
        </DropdownButton>

      </div>
        <div style={{display: 'flex', flexDirection: 'row'}}>
            <div className="deals-container" style={{width: '85%'}}>
                    {dealsData.map((deals) => (
                        <div key={deals.Title} className="col-md-auto col-md-auto bg-dark" style={{display: 'inline-block', width: '265px', height: '500px'}}>
                            <div className="card" style={{boxShadow: '1px 2px 3px 4px rgba(112,128,144,0.4)', margin:'10px 10px 50px 10px'}}>
                                <img className="product-image" src={String(deals.Image)} style={{width: 'auto', height: '200px'}}/>
                                <div className="card-body inner-card" style={{backgroundColor: '#3A373F', height:'250px'}}>
                                    <h5 className="text-center deal-title">{deals.Title}</h5>
                                        <h6 className="card-text deal-info" >Store: {deals.StoreName}</h6>
                                        <h6 className="card-text deal-info">Deal rating: {deals.DealRating}</h6>
                                        <p className="card-text text-center price-info"><strong>Sale price: $ {String(deals.SalePrice)}</strong></p>
                                        <p className="card-text text-center price-info"><strong>Normal price: $ {String(deals.NormalPrice)}</strong></p>
                                </div>
                            </div>
                            <br/>
                        </div>
                       
                    ))}
                </div>
            </div>
        </div>
    )

  return dealsPageContent;
}

Deals.prototype = {
    fetchData: PropTypes.func
}

export default Deals