"use client"
import React, {useEffect, useState, useMemo, useCallback } from "react";
import {Table, TableHeader, TableColumn, TableBody, TableRow, TableCell, Chip, Pagination, SortDescriptor} from "@nextui-org/react";


export default function Condition() {

    const [conditionData, setConditionData] = useState([])
    const [page, setPage] = useState(1);
    const [sortDescriptor, setSortDescriptor] = useState({
      column: "description",
      direction: 'ascending'})
    
    const rowsPerPage = 10;
    const pages = Math.ceil(conditionData.length / rowsPerPage);

    const statusColorMap = {
      Good: "success",
      Poor: "danger",
      Moderate: "warning",
      High: "primary",
      "Poor ecological potential": "danger",
      "Good ecological potential": "success",
      "Moderate ecological potential": "warning",
    };

    const columns = [
      {name: "Location", uid: "name"},
      {name: "Description", uid: "impactedCondition"},
      {name: "Current Condition", uid: "currentCondition"},
      {name: "2027 Target", uid: "targetCond2027"},
    ];

    useEffect(() => {
        const fetchData = async () => {
            const res = await fetch("https://localhost:7064/api/Dashboard/ConditionData")
            if(!res.ok){
                console.error("Bad response")
            }

            const data = await res.json();
            console.log("condition: ", data)
            setConditionData(data)
        }

        fetchData();
    }, [])

    const renderCell = useCallback((conditionData, columnKey) => {
    const cellValue = conditionData[columnKey];

    switch (columnKey) {
      case "name":
        return (
            <div>
                <p className="text-bold text-sm capitalize">{cellValue}</p>
            </div>
        );
      case "impactedCondition":
        return (
          <div className="flex flex-col">
            <p className="text-bold text-sm capitalize">{cellValue}</p>
          </div>
        );
      case "currentCondition":
        return (
          <Chip className="capitalize" color={statusColorMap[conditionData.currentCondition]} size="sm" variant="flat">
            {cellValue}
          </Chip>
        );
        case "targetCond2027":
        return (
          <Chip className="capitalize" color={statusColorMap[conditionData.targetCond2027]} size="sm" variant="flat">
            {cellValue}
          </Chip>
        );
      default:
        return cellValue;
    }
    }, []);

    const items = useMemo(() => {
      const start = (page - 1) * rowsPerPage;
      const end = start + rowsPerPage;

      return conditionData.slice(start, end);
    }, [page, conditionData]);
  

  return (
    <div className = "flex justify-center bg-athensGray ">
    <Table 
      aria-label="Condition table"
      className="w-[70%] flex justify-center"
      bottomContent={
          <div className="flex w-full justify-center rounded-lg shadow-2xl">
            <Pagination 
              isCompact
              showControls
              showShadow
              color="secondary"
              className="bg-viking"
              page={page}
              total={pages}
              onChange={(page) => setPage(page)}
              />  
          </div>}
      >
        <TableHeader columns={columns}>
          {(column) => (
            <TableColumn key={column.uid}
              align="start"
              className="bg-catalina text-white font-bold">
              {column.name}
            </TableColumn>
          )}
        </TableHeader>
        <TableBody items={items}>
          {(item) => (
            <TableRow key={item.id}>
              {(columnKey) => <TableCell>{renderCell(item, columnKey)}</TableCell>}
            </TableRow>
          )}
        </TableBody>
      </Table>
    </div>
  );
}
