import React, { useEffect, useState } from "react";
import Axios from "axios"
import {
    Layout,
    Menu,
    theme,
    Typography,
    Steps,
    Card,
    Space,
    Image,
    Row,
    Col,
  } from "antd";




var axios = Axios.create({
  baseURL: "https://localhost:7149"
});


export function SelectCar()
{
    const[cars, setCars] = useState<Car[]>([])


    useEffect(()=> {
      axios.post("/available-vehicles").then(res=> {
        setCars(res.data);
       });
      
    },[])
 
     
    
    console.log({cars})
    return (<>
    {cars.map(c=> 
        <AvailableCarItem
        car={c}/>
    )}
    </>)
    
}
export interface Car {
    vehicleID: number;
    registrationNumber: string;
    typeName: string;
  }
  
  function AvailableCarItem(props: { car: Car; onSelect?: () => void }) {
    const { car, onSelect } = props;
    return (
      <Card
      
        title={car.typeName}
        extra={<a onClick={onSelect}>Select</a>}
        style={{ margin: "20px 40px"}}
      >
        <Row >
          <Col span={12}>
            <p>
              <b>License number </b>
              {car.registrationNumber}
            </p>
            <p>Other generic info </p>
          </Col>
          <Col span={12}>
            <Image style={{height: "400px", width:"400px" }}  src={`/${car.vehicleID}.webp`   } 
             /> 
          </Col>
        </Row>
      </Card>
    );
  }
  