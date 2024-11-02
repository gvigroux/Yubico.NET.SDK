// Copyright 2021 Yubico AB
//
// Licensed under the Apache License, Version 2.0 (the "License").
// You may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Collections;
using System.Linq;
using System.Runtime.InteropServices;
using Yubico.Core.Iso7816;

namespace Yubico.YubiKey.Fido2.Commands
{
    /// <summary>
    /// Response to a command to get the firmware version.
    /// </summary>
    /// <remarks>
    /// <p>
    /// This is the partner Response class to <see cref="ThalesSerialNumberCommand"/>.
    /// </p>
    /// <p>
    /// The data returned is <see cref="FirmwareVersion"/>.
    /// </p>
    /// </remarks>
    internal class ThalesSerialNumberResponse : Fido2Response, IYubiKeyResponseWithData<string>
    {
        private const int expectedResponseLength = 10;

        public ThalesSerialNumberResponse(ResponseApdu responseApdu) :
            base(responseApdu)
        {

        }

        /// <inheritdoc/>
        public string GetData()
        {
            if (ResponseApdu.SW != SWConstants.Success)
            {
                throw new InvalidOperationException(ExceptionMessages.NoResponseDataApduFailed);
            }

            if (ResponseApdu.Data.Length != expectedResponseLength)
            {
                throw new MalformedYubiKeyResponseException(ExceptionMessages.UnknownFidoError);
            }

            byte[] serialNumberBytes = MemoryMarshal.AsBytes(ResponseApdu.Data.Span.Slice(2)).ToArray();
            return System.Text.Encoding.Default.GetString(serialNumberBytes);     
        }
    }
}
