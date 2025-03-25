import { useEffect, useState } from "react"
import  { appsettings } from "../settings/appsettings"
import { Link } from "react-router-dom"
import { ICompany } from "../interface/ICompany"
import { Container, Row, Col, Table, Button } from "reactstrap"
import Swal from "sweetalert2"

export function List() {
    const [companies, setCompanies] = useState<ICompany[]>([]);

    const getCompanies = async () =>{
        const response = await fetch(`${appsettings.apiUrl}api/Company/list`)
        if(response.ok){
            const data = await response.json();
            setCompanies(data);
        }
    }

    useEffect(() => {getCompanies()}, []);

    const deleteCompany = (id: number) => {
        Swal.fire({
            title: "Are you sure?",
            text: "Delete company!",
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Delete it!'
        }).then(async(result) => {
            if(result.isConfirmed){
                const response = await fetch(`${appsettings.apiUrl}api/Company/delete/${id}`, {method:"DELETE"})
                if(response.ok) await getCompanies();
            }
        });
    }

    return(
        <Container className="mt-5">
            <Row>
                <Col sm={{size:8, offset:2}}>
                    <h4>Company List</h4>
                    <hr/>
                    <Link className="btn btn-success mb-3" to="/newcompany">Add</Link>
                    <Table bordered>
                        <thead>
                            <tr>
                                <th>Name</th>
                                <th>Exchange</th>
                                <th>Ticker</th>
                                <th>Isin</th>
                                <th>Website</th>
                            </tr>
                        </thead>
                        <tbody>
                            {
                                companies.map((company) => (
                                    <tr key={company.id}>
                                        <td>{company.name}</td>
                                        <td>{company.exchange}</td>
                                        <td>{company.stockTicker}</td>
                                        <td>{company.isin}</td>
                                        <td>{company?.websiteurl?.valueOf() ?? 'N/A'}</td>
                                        <td>
                                            <Link className="btn btn-primary mb-2" to={`/editcompany/${company.id}`}>Edit</Link>
                                        </td>
                                        <td>
                                            <Button color="danger" onClick={() => {
                                                deleteCompany(company.id)
                                            }}>Remove</Button>
                                        </td>
                                    </tr>
                                ))
                            }
                        </tbody>
                    </Table>
                </Col>
            </Row>
        </Container>
    )
}
