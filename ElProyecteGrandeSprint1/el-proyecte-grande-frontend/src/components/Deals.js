import React, { Component, useEffect } from 'react';
import PropTypes from 'prop-types'
import { useState } from "react";

const Deals = ({fetchData, showDeals}) => {
    const [dealsData, setDealsData] = useState([]);

    useEffect(() => {
        fetchData('https://localhost:7064/Deals/60')
        .then(dealsData => {
            setDealsData(dealsData);
        })
    }, [])

    const dealsPageContent = 
    (<div className="container">
        <div style={{display: 'flex', flexDirection: 'row'}}>
            <div className="deals-container" style={{width: '100%'}}>
                    {dealsData.map((deals) => (
                        <div className="col-md-auto col-md-auto bg-dark" style={{display: 'inline-block', width: '300px', height: 'auto'}}>
                            <div className="card" style={{boxShadow: '1px 2px 3px 4px rgba(112,128,144,0.4)', margin:'10px 10px 10px 20px'}}>
                                <img className="product-image" src={String(deals.Image)} style={{maxWidth: 'auto', maxHeight: 'auto'}}/>
                                <div className="card-body inner-card" style={{backgroundColor: '#3A373F'}}>
                                    <h5 className="card-title text-center">{deals.Title}</h5>
                                        <p className="card-text">Store name: {deals.StoreName}</p>
                                        <p className="card-text">Deal rating: {deals.DealRating}</p>
                                        <p className="card-text text-center"><strong>Sale price: $ {String(deals.SalePrice)}</strong></p>
                                        <p className="card-text text-center"><strong>Normal price: $ {String(deals.NormalPrice)}</strong></p>
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
    fetchData: PropTypes.func,
    showDeals: PropTypes.func
}

export default Deals