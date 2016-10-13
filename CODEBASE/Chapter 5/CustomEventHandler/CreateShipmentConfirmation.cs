using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.SensorServices.Rfid;

namespace CustomEventHandler
{
    public class CreateShipmentConfirmation: RfidEventHandlerBase
    {
        private string warehouseName;
        public override void Init(Dictionary<string, object> parameters, 
            RfidProcessContext container)
        {
            warehouseName = (string) parameters[WarehouseNameKey];
        }

        [RfidEventHandlerMethod]
        public ShipmentEvent TransformToShipmentEvent(TagReadEvent tre)
        {
            //todo: lookup local business context to populate it
            string purchaseOrderId = "this will come from the backend"; 

            return new ShipmentEvent(tre, purchaseOrderId, warehouseName);
        }

        public static RfidEventHandlerMetadata GetEventHandlerMetadata(
            bool includeVendorExtensions)
        {
            Dictionary<string, RfidEventHandlerParameterMetadata> 
                parameterMetadata = 
                new Dictionary<string, RfidEventHandlerParameterMetadata>();
            parameterMetadata[WarehouseNameKey] = 
                new RfidEventHandlerParameterMetadata(typeof(string),
                "the name of the warehouse in which this handler is deployed",
                null /*default*/, true /*isMandatory*/);
            return new RfidEventHandlerMetadata(
                "Converts rfid tag read events into shipment confirmation events",
                parameterMetadata);
        }
        private const string WarehouseNameKey = "Warehouse Name";
    }

    [DataContract]
    public class ShipmentEvent: RfidEventBase
    {
        public string PurchaseOrderId
        {
            get { return purchaseOrderId; }
            set { purchaseOrderId = value; }
        }

        public byte[] TagId
        {
            get { return tagId; }
            set { tagId = value; }
        }

        public string OriginationWarehouse
        {
            get { return originationWarehouse; }
            set { originationWarehouse = value; }
        }

        public DateTime CreationTime
        {
            get { return creationTime; }
            set { creationTime = value; }
        }

        public ShipmentEvent(TagReadEvent tre, string purchaseOrderId, 
            string warehouseName)
        {
            tagId = tre.GetId();
            this.purchaseOrderId = purchaseOrderId;
            originationWarehouse = warehouseName;
        }

        [DataMember]
        private string purchaseOrderId;
        [DataMember]
        private byte[] tagId;
        [DataMember]
        private string originationWarehouse;
        [DataMember]
        private DateTime creationTime;
    }
}
