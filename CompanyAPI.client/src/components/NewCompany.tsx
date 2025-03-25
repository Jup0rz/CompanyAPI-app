import { ChangeEvent, useState } from "react"
import  { appsettings } from "../settings/appsettings"
import { useNavigate } from "react-router-dom"
import Swal from "sweetalert2"
import { ICompany } from "../interface/ICompany"
import { Container, Row, Col, Form, FormGroup, Label, Input, Button } from "reactstrap"

const initialCompany = {
    id: 0,
    name: "",
    stockTicker: "",
    exchange: "",
    isin: "",
    websiteurl: null
}

export function NewCompany(){
    const [company, setCompany] = useState<ICompany>(initialCompany);
    const navigate = useNavigate();

    const inputChangeValue = (event : ChangeEvent<HTMLInputElement>) => {
        const inputName = event.target.name;
        const inputValue = event.target.value === '' ? null : event.target.value;

        setCompany({...company, [inputName] : inputValue})
    }

    const saveCompany = async () =>{
        const response = await fetch(`${appsettings.apiUrl}api/Company/create`, {
            method: 'POST',
            headers:{
                'Content-Type':'application/json'
            },
            body: JSON.stringify(company)
        })
        if(response.ok){
            navigate("/");
        }
        else{
            Swal.fire({
                title: "Error",
                text: "Error trying to create company",
                icon: "error"
            })
        }
    }

    const returnHome = () =>{
        navigate("/");
    }


    return(
        <Container className="mt-5">
            <Row>
                <Col sm={{size:8, offset:2}}>
                <h4>New Company</h4>
                <hr/>
                <Form>
                <FormGroup>
                    <Label>Name</Label>
                    <Input type="text" name="name" onChange={inputChangeValue} value={company.name}/>
                </FormGroup>
                <FormGroup>
                    <Label>StockTicker</Label>
                    <Input type="text" name="stockTicker" onChange={inputChangeValue} value={company.stockTicker}/>
                </FormGroup>
                <FormGroup>
                    <Label>Exchange</Label>
                    <Input type="text" name="exchange" onChange={inputChangeValue} value={company.exchange}/>
                </FormGroup>
                <FormGroup>
                    <Label>Isin</Label>
                    <Input type="text" name="isin" onChange={inputChangeValue} value={company.isin}/>
                </FormGroup>
                <FormGroup>
                    <Label>WebSite</Label>
                    <Input type="text" name="websiteurl" onChange={inputChangeValue} value={company?.websiteurl?.toString()}/>
                </FormGroup>
                </Form>
                <Button color="primary" className="me-4" onClick={saveCompany}>Save</Button>
                <Button color="secondary" onClick={() => {returnHome()}}>Return</Button>
                </Col>
            </Row>
        </Container>
    )
}